

async function UserAchievementsGet(params) {
    try {
        const username = localStorage.getItem('userName');
        if (!username) {
            alert('No estás logueado. Inicia sesión para ver tus logros.');
            return; // Salimos de la función si no hay usuario
        }

        const response = await fetch(`https://localhost:7276/api/UserAchievements/AchievementUser?username=${username}`);

        
        if(!response.ok){
            throw new error ('no se pudo obtener los datos necesarios de la API')
        }
        const data = await response.json();
        const includeData = data.data[0].include;

        const comboBox = document.getElementById('achievementsDropdown');
        if (!comboBox) {
            throw new Error('No se encontró el comboBox');
        }
        
        if (includeData.length === 0) {
            // validacion para ver si se cargó algun logro
            alert('¡No tienes logros aún! Comienza a completar tareas para ganar logros.');
        } else {
            // si no entra al if, se cargan los logros q trae el GET
            includeData.forEach(item => {
                const option = document.createElement('option');
                option.value = item.id; //valor unico de cada logro
                option.textContent = item.name;  // el nombre de logro q se vé en el desplegable
                comboBox.appendChild(option);
            });
        };

    } catch (error) {
        console.error('error al cargar los datos', error)
    }
}