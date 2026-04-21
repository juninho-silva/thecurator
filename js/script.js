const API_BASE_URL = 'https://thecurator.railway.internal/api/v1';
const API_URL = `${API_BASE_URL}`;

// Carregar gêneros e frequências quando a página carregar
document.addEventListener('DOMContentLoaded', async () => {
    try {
        await Promise.all([
            loadMovieGenres(),
            loadSeriesGenres(),
            loadFrequencies()
        ]);
    } catch (error) {
        console.error('Erro ao carregar dados:', error);
        showError('Erro ao carregar formulário. Tente recarregar a página.');
    }
});

async function loadMovieGenres() {
    try {
        const res = await fetch(`${API_BASE_URL}/contents/genres-movies`);
        const genres = await res.json();
        renderGenres(genres, 'movieGenres', 'movieGenresContainer');
    } catch (error) {
        console.error('Erro ao carregar gêneros de filmes:', error);
    }
}

async function loadSeriesGenres() {
    try {
        const res = await fetch(`${API_BASE_URL}/contents/genres-tv`);
        const genres = await res.json();
        renderGenres(genres, 'seriesGenres', 'seriesGenresContainer');
    } catch (error) {
        console.error('Erro ao carregar gêneros de séries:', error);
    }
}

async function loadFrequencies() {
    try {
        const res = await fetch(`${API_BASE_URL}/contents/frequencies-in-days`);
        const frequencies = await res.json();
        renderFrequencies(frequencies);
    } catch (error) {
        console.error('Erro ao carregar frequências:', error);
    }
}

function renderGenres(genres, groupName, containerId) {
    const container = document.getElementById(containerId);
    container.innerHTML = '';
    
    genres.forEach((genre) => {
        const id = `${groupName}-${genre.id}`;
        const div = document.createElement('div');
        div.className = 'checkbox-item';
        div.innerHTML = `
            <input type="checkbox" id="${id}" name="${groupName}" value="${genre.id}" />
            <label for="${id}">${genre.name}</label>
        `;
        container.appendChild(div);
    });
}

function renderFrequencies(frequencies) {
    const container = document.getElementById('frequenciesContainer');
    container.innerHTML = '';
    
    // Map português para os valores dos enums do backend
    const frequencyMap = {
        'Sunday': 'Domingo',
        'Monday': 'Segunda',
        'Tuesday': 'Terça',
        'Wednesday': 'Quarta',
        'Thursday': 'Quinta',
        'Friday': 'Sexta',
        'Saturday': 'Sábado'
    };
    
    frequencies.forEach((freq, index) => {
        const id = `freq-${freq.id}`;
        const displayName = frequencyMap[freq.name] || freq.name;
        const isChecked = freq.name === 'Friday' ? 'checked' : ''; // Sexta-feira como padrão
        
        const div = document.createElement('div');
        div.className = 'radio-item';
        div.innerHTML = `
            <input type="radio" id="${id}" name="frequency" value="${freq.name}" ${isChecked} />
            <label for="${id}">${displayName}</label>
        `;
        container.appendChild(div);
    });
}

async function handleSubmit() {
    const name = document.getElementById('name').value.trim();
    const email = document.getElementById('email').value.trim();
    const btn = document.getElementById('submit-btn');
    const err = document.getElementById('error-msg');
    const spinner = document.getElementById('spinner');

    err.style.display = 'none';

    if (!name) return showError('Coloca seu nome pra eu saber quem é você 😊');
    if (!email || !email.includes('@')) return showError('E-mail inválido.');

    // Coletar gêneros de filmes selecionados
    const movieGenres = Array.from(document.querySelectorAll('input[name="movieGenres"]:checked'))
        .map(checkbox => parseInt(checkbox.value));

    // Coletar gêneros de séries selecionados
    const seriesGenres = Array.from(document.querySelectorAll('input[name="seriesGenres"]:checked'))
        .map(checkbox => parseInt(checkbox.value));

    // Coletar frequência selecionada
    const frequency = document.querySelector('input[name="frequency"]:checked').value;

    btn.disabled = true;
    spinner.style.display = 'inline-block';
    document.querySelector('.btn-text').textContent = 'Enviando...';

    try {
        const res = await fetch(`${API_BASE_URL}/events/subscriber`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ 
                name, 
                email,
                genres_movies: movieGenres,
                genres_series: seriesGenres,
                frequency
            })
        });

        const data = await res.json();

        if (!res.ok) throw new Error(data.message || 'Algo deu errado.');

        document.getElementById('success-name').textContent = name.split(' ')[0];
        document.getElementById('form-area').style.display = 'none';
        document.getElementById('success-area').style.display = 'block';

    } catch (e) {
        showError(e.message);
        btn.disabled = false;
        spinner.style.display = 'none';
        document.querySelector('.btn-text').textContent = 'Quero receber';
    }
}

function showError(msg) {
    const err = document.getElementById('error-msg');
    err.textContent = msg;
    err.style.display = 'block';
}

// Enter key
document.addEventListener('keydown', e => {
    if (e.key === 'Enter') handleSubmit();
});