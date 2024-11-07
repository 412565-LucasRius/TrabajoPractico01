
document.getElementById('achievementsDropdown').addEventListener('change', UserAchievementsGet);
async function UserAchievementsGet() {
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

UserAchievementsGet()

// Función para obtener el ID de usuario
function getUserId() {
  const userId = localStorage.getItem('userId');
  if (userId) {
    return userId;
  }

  // Si no hay ID de usuario en el localStorage, devuelve 0
  return 0;
}

// Función para obtener los datos de la base de datos y actualizar la página
async function updateMovieData() {
  try {
    const userId = getUserId();

    // Conectar a la base de datos y obtener los datos
    const response = await fetch(`https://localhost:7276/api/UserGenre/userGenresMovie?userId=${userId}`);
    const movieData = await response.json();

    // Calcular el total de películas vistas
    const totalMoviesWatched = movieData.reduce((total, data) => total + data.viewCount, 0);

    // Actualizar el contador de películas
    document.querySelector('.count').textContent = totalMoviesWatched;

    // Calcular los puntos totales
    let totalPoints = 0;
    const achievementElements = document.querySelectorAll('.achievement');
    achievementElements.forEach((achievement, index) => {
      const achievementPoints = parseInt(achievement.querySelector('.achievement-points').textContent);
      const achievementThreshold = parseInt(achievement.querySelector('.achievement-content p').textContent.match(/\d+/)[0]);
      const isUnlocked = movieData.some(data => data.viewCount >= achievementThreshold);
      if (isUnlocked) {
        achievement.querySelector('.achievement-icon').innerHTML = '<i class="fas fa-check"></i>';
        totalPoints += achievementPoints;
      }
    });

    // Actualizar el marcador de puntos totales
    document.querySelector('.points').textContent = totalPoints;
  } catch (error) {
    console.error('Error al obtener los datos de la base de datos:', error);
  }
}

// Ejecutar la función para actualizar los datos inicialmente
updateMovieData();
