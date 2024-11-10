    // Función para obtener los datos del perfil de usuario (incluyendo la imagen desde la API)
    async function LoadUserProfile() {
      const userId = localStorage.getItem("userId");
      const token = localStorage.getItem("JWT-Token");

      if (!userId || !token) {
          console.error("Usuario no autenticado.");
          return;
      }

      try {
          const response = await fetch(`https://localhost:7276/api/User/byid?userAccountId=${userId}`, {
              method: 'GET',
              headers: {
                  "Authorization": `Bearer ${token}`,
                  "Content-Type": "application/json"
              }
          });

          if (response.ok) {
              const userData = await response.json();
              // Actualiza el nombre, email de perfil
              document.getElementById("userName").textContent = userData.username;
              document.getElementById("userEmail").textContent = userData.email;
          }
      } catch (error) {
          console.error("Error al hacer la solicitud:", error);
      }
  }

  // Función para obtener las reservas del usuario
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
              const reservationsList = document.getElementById("reservationsList");
              reservationsList.innerHTML = ""; // Limpiar la lista actual

              // Recorremos las reservas y creamos el HTML dinámicamente
              bookings.forEach(booking => {
                  const bookingElement = document.createElement("tr");

                  // Crear el contenido de la reserva
                  const bookingDetails = `
                      <td>${new Date(booking.bookingDate).toLocaleDateString()}</td>
                      <td>${booking.status}</td>
                      <td>${booking.movieTitle}</td>
                      <td>${booking.cinemaLocation}</td>
                      <td>${booking.seats}</td> <!-- Nueva celda para los asientos -->
                      <td>
                          <button class="update-reservation" onclick="updateReservation(${booking.id})">Actualizar</button>
                          <button class="delete-reservation" onclick="deleteReservation(${booking.id})">Eliminar</button>
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

  // Función para actualizar una reserva (ejemplo de acción)
  function updateReservation(bookingId) {
      alert(`Actualizar reserva con ID: ${bookingId}`);
      // Aquí se podría agregar lógica para redirigir o mostrar un formulario para editar la reserva
  }

  // Función para eliminar una reserva (ejemplo de acción)
  function deleteReservation(bookingId) {
      if (confirm(`¿Estás seguro de que deseas eliminar esta reserva?`)) {
          console.log(`Eliminando reserva con ID: ${bookingId}`);
          // Aquí se podría agregar lógica para eliminar la reserva mediante una solicitud a la API
      }
  }

  // Llamamos a las funciones cuando el DOM esté completamente cargado
  window.addEventListener("DOMContentLoaded", LoadUserProfile);
  window.addEventListener("DOMContentLoaded", FetchUserBookings);