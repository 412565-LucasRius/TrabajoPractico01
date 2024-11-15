
document.addEventListener('DOMContentLoaded', () => {
    initializeAchievements();
    CheckUserSession();
});

async function initializeAchievements() {
    try {
        const userId = localStorage.getItem('userId');

        const response = await fetch(`https://localhost:7276/api/Booking/UniqueBookingCounts?userId=${userId}&genreId=2`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
            }
        });
        if (!response.ok) {
            throw new Error('No se pudo obtener los datos necesarios de la API');
        }

        const logros = await response.json();
        console.log('Logros recibidos:', logros);

        console.log('Logros recibidos:', JSON.stringify(logros) );

        const response2 = await fetch(`https://localhost:7276/api/UserAchievements/GetAchievementsByIds`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(logros)
        });

        if (!response2.ok) {
            throw new Error('No se pudo obtener los datos necesarios de la API');
        }

        const achievements = await response2.json();
        console.log('Logros recibidos:', achievements);

        updateCounters(achievements);
        displayAchievements(achievements);

        setInterval(() => initializeAchievements(), 30000);

    } catch (error) {
        console.error('Error al cargar los datos:', error);
        const container = document.getElementById('achievementsContainer');
        if (container) {
            container.innerHTML = '<p class="error-message">Error al cargar los logros. Por favor, intenta más tarde.</p>';
        }
    }
}


function updateCounters(achievements) {
    
    const pointsCounter = document.querySelector('.points');
    const movieCounter = document.querySelector('.count');
    
    const totalPoints = achievements.reduce((sum, achievement) => sum + achievement.points, 0);
    
    pointsCounter.textContent = totalPoints;
    movieCounter.textContent = achievements.length;
}


function displayAchievements(achievements) {
    const container = document.getElementById('achievementsContainer');
    container.innerHTML = '';

    if (achievements.length === 0) {
        container.innerHTML = '<p class="no-achievements">¡No tienes logros aún!</p>';
        return;
    }

    function getIconForAchievement(name) {
        if (name.includes('Novato')) return 'fa-solid fa-star';
        if (name.includes('Experto')) return 'fa-solid fa-trophy';
        if (name.includes('Terror')) return 'fa-solid fa-ghost';
        return 'fa-solid fa-award'; 
    }

    achievements.forEach(achievement => {
        const achievementElement = document.createElement('div');
        achievementElement.className = 'achievement';

        const icon = getIconForAchievement(achievement.name); 

        achievementElement.innerHTML = `
            <div class="achievement-icon">
                <i class="${icon}"></i>
            </div>
            <div class="achievement-content">
                <h3>${achievement.name}</h3>
                <p class="achievement-description">${achievement.description}</p>
            </div>
            <div class="achievement-points">
                ${achievement.points} pts
            </div>
        `;

        container.appendChild(achievementElement);
    });
}

async function CheckUserSession() {
    const userId = localStorage.getItem('userId');
    const token = localStorage.getItem('JWT-Token');


    if (!userId || !token) {
        window.location.href = 'login.html';
    } else {
        document.getElementById("userIconLink").setAttribute('href', 'perfil.html');
    }
}