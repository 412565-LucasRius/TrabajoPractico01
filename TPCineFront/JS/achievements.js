async function fetchAchievements() {
  try {
    const response = await fetch('https://localhost:7276/api/Achievements', {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
      }
    });

    if (response.ok) {
      const achievements = await response.json();
      console.log(achievements);

      displayAchievements(achievements);
    } else {
      console.error("Error al obtener los logros:", response.statusText);
    }
  } catch (error) {
    console.error("Ocurrió un error:", error.message);
  }
}

function displayAchievements(achievements) {
  const achievementsContainer = document.getElementById("achievementsContainer");
  achievementsContainer.innerHTML = "";

  achievements.forEach(achievement => {
    const achievementElement = document.createElement("div");
    achievementElement.className = "achievement";
    achievementElement.innerHTML = `
      <h3>${achievement.name}</h3>
      <p>${achievement.description}</p>
      <p>Puntos: ${achievement.points}</p>
    `;
    achievementsContainer.appendChild(achievementElement);
  });
}

window.addEventListener("DOMContentLoaded", fetchAchievements)

async function createAchievementUser(achievementId) {
  const userId = localStorage.getItem('userId');

  const requestBody = {
    UserId: userId,
    AchievementId: achievementId
  };

  try {
    const response = await fetch('https://tu-api-url/AchievementUser', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${tuTokenDeAutenticacion}`
      },
      body: JSON.stringify(requestBody)
    });

    if (response.ok) {
      alert("Logro obtenido exitosamente");
    } else if (response.status === 400) {
      const error = await response.text();
      alert(error);
    } else {
      throw new Error("Error en la solicitud");
    }
  } catch (error) {
    console.error("Ocurrió un error:", error.message);
    alert("Error al obtener el logro.");
  }
}
