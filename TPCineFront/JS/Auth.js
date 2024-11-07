const API_URL = 'https://localhost:7276/api/User'

async function login() {
  const username = document.getElementById("username").value;
  const password = document.getElementById("password").value;

  try {
    const response = await fetch(`${API_URL}/login`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({ username, password })
    });

    if (!response.ok)
      throw new Error('Usuario o contraseña incorrectos.')

    const data = await response.json();

    const userId = data.userId
    const token = data.token;

    localStorage.setItem('JWT-Token', token)
    localStorage.setItem('userId', userId)

    window.location.href = 'index.html'
  } catch (error) {
    console.error("Error en el inicio de sesión:", error.message);
    alert("Hubo un error al iniciar sesión: " + error.message);
  }
}