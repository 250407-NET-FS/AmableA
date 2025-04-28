
const ul = document.getElementById('challenge4');
const input = document.getElementById('challenge');
const button = document.getElementById('challengeSubmit');

button.addEventListener('click', () => {
    const text = input.value.trim();
    if (!text) return;

    const li = document.createElement('li');
    li.textContent = text;
    ul.appendChild(li);

    input.value = '';

    
});

