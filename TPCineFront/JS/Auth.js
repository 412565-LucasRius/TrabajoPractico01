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

    if (!response.ok) {
      const errorData = await response.json();
      throw new Error(errorData.message || 'Usuario o contraseña incorrectos.');
    }

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

document.getElementById('registerButton').addEventListener('click', async () => {
const name = document.getElementById('name').value.trim(); 
const username = document.getElementById('username').value.trim(); 
const email = document.getElementById('email').value.trim();    
const password = document.getElementById('password').value.trim(); 
let borndate = document.getElementById('birthdate').value.trim();
borndate = new Date(borndate).toISOString().split('T')[0]; 
try {
const requestBody = {
  name,
  username,
  email,
  password,
  borndate,
};
const response = await fetch('https://localhost:7276/api/User/register', {
  method: 'POST', 
  headers: {
    'Content-Type': 'application/json', 
  },
  body: JSON.stringify(requestBody),
});
  if (response.ok) {
    const data = await response.json();
    console.log('Usuario registrado:', data);
    alert('Registro exitoso');
    window.location.href = 'login.html';
  } else {
    const errorData = await response.json();
    alert("Error al registrar el usuario: " + (errorData.message || "Error desconocido"));
  }
} catch (error) {
  console.error("Error en el registro:", error.message);
  alert("Hubo un error al registrar el usuario: " + error.message);
}
});


async function logout() {
  localStorage.removeItem('JWT-Token')
  localStorage.removeItem('userId')

  window.location.href = 'login.html'
}