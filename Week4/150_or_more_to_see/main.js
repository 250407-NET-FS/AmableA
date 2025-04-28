document.addEventListener('DOMContentLoaded', () => {
    const fakeBtn       = document.getElementById('fakeFetchBtn');
    const dataContainer = document.getElementById('dittoData');
  

    function fetchDitto() {
      fetch('https://pokeapi.co/api/v2/pokemon/ditto')
        .then(res => {
          if (!res.ok) throw new Error(res.statusText);
          return res.json();
        })
        .then(data => {
          dataContainer.innerHTML = `
            <h3>${data.name}</h3>
            <p><strong>Height:</strong> ${data.height}</p>
            <p><strong>Weight:</strong> ${data.weight}</p>
            <p><strong>Types:</strong> ${data.types
              .map(t => t.type.name) //without mapping its object object
              .join(', ')
            }</p>
            <p><strong>Abilities:</strong> ${data.abilities
              .map(a => a.ability.name)
              .join(', ')
            }</p>
          `;
        })               
        .catch(err => {
          dataContainer.textContent = 'Even his data is hiding from you: ' + err;
        });
    }
  

    fakeBtn.addEventListener('click', () => {
      const img = new Image();
      img.src = 'pikaditto.avif';
      img.alt = 'Ditto?';
      fakeBtn.replaceWith(img);
  
      const realBtn = document.createElement('button');
      realBtn.id = 'fetchBtn';
      realBtn.textContent = 'Fetch actual Ditto data';
      img.insertAdjacentElement('afterend', realBtn);
  
      realBtn.addEventListener('click', fetchDitto);
    });
  });
  