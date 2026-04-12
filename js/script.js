const API_URL = 'https://SEU-BACKEND.railway.app/subscribe';

async function handleSubmit() {
    const name = document.getElementById('name').value.trim();
    const email = document.getElementById('email').value.trim();
    const btn = document.getElementById('submit-btn');
    const err = document.getElementById('error-msg');
    const spinner = document.getElementById('spinner');

    err.style.display = 'none';

    if (!name) return showError('Coloca seu nome pra eu saber quem é você 😊');
    if (!email || !email.includes('@')) return showError('E-mail inválido.');

    btn.disabled = true;
    spinner.style.display = 'inline-block';
    document.querySelector('.btn-text').textContent = 'Enviando...';

    try {
        const res = await fetch(API_URL, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ name, email })
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