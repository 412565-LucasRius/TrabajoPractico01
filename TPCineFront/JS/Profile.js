async function LoadUserProfile() {
  const userId = localStorage.getItem("userId");
  const token = localStorage.getItem("JWT-Token");

  if (!userId || !token) {
    console.error("Usuario no autenticado.");
    window.location.href = 'login.html';
  }

  try {
    const response = await fetch(`https://localhost:7276/api/User/byid?userAccountId=${userId}`, {
      method: 'GET',
      headers: {
        "Authorization": `Bearer ${token}`,
        "Content-Type": "application/json"
      }
    })

    if (response.ok) {
      const userData = await response.json();

      document.getElementById("userName").textContent = userData.username
      document.getElementById("userEmail").textContent = userData.email

    }
  } catch (error) {
    console.error("Error al hacer la solicitud:", error);
  }
}

window.addEventListener("DOMContentLoaded", LoadUserProfile)
window.addEventListener("DOMContentLoaded", FetchUserBookings)

// Función para abrir el modal
function openModal() {
  document.getElementById('editUsernameModal').style.display = 'block';
}

// Función para cerrar el modal
function closeModal() {
  document.getElementById('editUsernameModal').style.display = 'none';
}

// Función para abrir el modal al hacer clic en el botón de "Actualizar Información"
document.querySelector('.update-button').addEventListener('click', openModal);


async function UpdateUserData() {
  const userId = localStorage.getItem("userId");
  const newUsername = document.getElementById('newUsername').value;

  if (!newUsername.trim()) {
    alert("El nombre de usuario no puede estar vacío.");
    return;
  }

  const token = localStorage.getItem("JWT-Token");

  if (!token || !userId) {
    alert("No se pudo encontrar el token o el ID del usuario.");
    console.log(token, userId);

    return;
  }

  const requestBody = {
    userAccountId: userId,
    newUsername: newUsername
  };

  try {
    const response = await fetch(`https://localhost:7276/api/User/update`, {
      method: 'PUT',
      headers: {
        "Authorization": `Bearer ${token}`,
        "Content-Type": "application/json"
      },
      body: JSON.stringify(requestBody)
    });

    if (response.ok) {
      const result = await response.json();
      alert("Nombre de usuario actualizado correctamente");
      document.getElementById('userName').textContent = newUsername;
      document.getElementById('editUsernameModal').style.display = 'none';
    } else {
      const errorData = await response.json();
      alert(`Error: ${errorData.message || 'No se pudo actualizar el nombre de usuario'}`);
    }
  } catch (error) {
    alert("Error al intentar actualizar el nombre de usuario: " + error.message);
  }
}


async function SetUserAsInactive() {
  const userId = localStorage.getItem("userId");
  const token = localStorage.getItem("JWT-Token");

  // Solicita confirmación antes de proceder
  const confirmDelete = confirm("¿Seguro que deseas eliminar el usuario?");
  if (!confirmDelete) return;

  try {
    const response = await fetch(`https://localhost:7276/api/User/deactivate?userId=${userId}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      }
    });

    if (response.ok) {
      alert("Usuario desactivado con éxito.");
      // Opcionalmente redirige al usuario o realiza otra acción después de la desactivación
      window.location.href = 'login.html';
    } else {
      const errorMessage = await response.text();
      console.error("Error al desactivar el usuario:", errorMessage);
      alert("Hubo un error al desactivar el usuario.");
    }
  } catch (error) {
    console.error("Error en la solicitud:", error.message);
    alert("Hubo un problema con la solicitud.");
  }
}


async function FetchUserBookings() {
  const userId = localStorage.getItem("userId");
  const token = localStorage.getItem("JWT-Token");

  if (!userId || !token) {
    console.error("Usuario no autenticado.");
    return;
  }

  try {
    const response = await fetch(`https://localhost:7276/api/Booking/GetBookingByUser?userId=${userId}`, {
      method: 'GET',
      headers: {
        "Authorization": `Bearer ${token}`,
        "Content-Type": "application/json"
      }
    });

    if (response.ok) {
      const bookings = await response.json();
      console.log(bookings);

      const reservationsList = document.getElementById("reservationsList");
      reservationsList.innerHTML = ""; // Limpiar la lista actual

      // Recorremos las reservas y creamos el HTML dinámicamente
      bookings.forEach(booking => {
        const bookingElement = document.createElement("tr");

        // Crear el contenido de la reserva
        const bookingDetails = `
          <td>${new Date(booking.bookingDate).toLocaleDateString()}</td>
          <td>${booking.tickets[0].showtime.screenId}</td>
          <td>${booking.tickets[0].showtime.movie.title}</td>
          <td>${booking.tickets.map(ticket => ticket.seatNumber).join(', ')}</td>
          <td>${booking.bookingState.description}</td> <!-- Columna de estado -->
          <td>
            <button class="update-reservation" onclick="updateReservation(${booking.bookingId})">Actualizar</button>
            <button class="delete-reservation" onclick="deleteReservation(${booking.bookingId})">Eliminar</button>
          </td>
        `;

        bookingElement.innerHTML = bookingDetails;
        reservationsList.appendChild(bookingElement);
      });
    } else {
      console.error("Error al obtener las reservas:", response.statusText);
    }
  } catch (error) {
    console.error("Error al hacer la solicitud:", error);
  }
}

async function deleteReservation(bookingId) {
  try {
    // Confirmar con el usuario
    const userConfirmed = confirm('¿Estás seguro de que deseas eliminar esta reserva?');
    if (!userConfirmed) return;

    const state = 3;
    console.log(`Eliminando reserva con ID: ${bookingId}, state: ${state}`);

    // Realizar la solicitud PUT
    const response = await fetch(`https://localhost:7276/api/Booking/DeleteBooking/${bookingId}/${state}`, {
      method: 'PUT'
    });

    // Verificar si la respuesta fue exitosa
    if (response.ok) {
      // Verificar si hay contenido en la respuesta
      const contentType = response.headers.get("content-type");
      if (contentType && contentType.includes("application/json")) {
        const responseData = await response.json();
        alert(responseData.message || "Reserva eliminada correctamente.");
      } else {
        alert("Reserva eliminada correctamente.");
      }
      
      await FetchUserBookings(); // Actualizar la lista de reservas
    } else {
      // Manejar error de la respuesta
      try {
        const errorData = await response.json();
        throw new Error(errorData.message || 'Error al eliminar la reserva');
      } catch (jsonError) {
        throw new Error(`Error ${response.status}: No se pudo eliminar la reserva`);
      }
    }

  } catch (error) {
    console.error("Error en la solicitud:", error);
    alert(error.message || "Hubo un problema al eliminar la reserva. Por favor, inténtalo de nuevo.");
  }
}

// Función para actualizar una reserva (ejemplo de acción)
function updateReservation(bookingId) {
  alert(`Actualizar reserva con ID: ${bookingId}`);
  // Aquí se podría agregar lógica para redirigir o mostrar un formulario para editar la reserva
}

// Llamamos a las funciones cuando el DOM esté completamente cargado
window.addEventListener("DOMContentLoaded", LoadUserProfile);
window.addEventListener("DOMContentLoaded", FetchUserBookings);