document.addEventListener("DOMContentLoaded", () => {

  const userId = localStorage.getItem("userId");
  const token = localStorage.getItem("JWT-Token")

  if (!userId || !token) {
    window.location.href = 'login.html';
  }
})

let selectedSeats = [];

document.addEventListener("DOMContentLoaded", () => {
  // Ejemplo de asientos: 1 = disponible, 0 = ocupado, 2 = seleccionado
  const seats = [
    [1, 1, 1, 1, 1],
    [1, 1, 1, 1, 1],
    [1, 1, 0, 1, 1],
    [1, 0, 1, 1, 1],
    [1, 1, 1, 1, 0]
  ];

  const seatingChart = document.querySelector('.seating-chart');
  const confirmBtn = document.getElementById('confirm-btn');


  // Crear los asientos en el DOM
  createSeats(seats);

  // Función para crear los asientos en el DOM
  function createSeats(seatsArray) {
    seatsArray.forEach((row, rowIndex) => {
      row.forEach((seat, seatIndex) => {
        const seatElement = document.createElement('div');
        seatElement.classList.add('seat');

        if (seat === 1) {
          seatElement.classList.add('available');
          seatElement.addEventListener('click', () => selectSeat(rowIndex, seatIndex, seatElement));
        } else if (seat === 0) {
          seatElement.classList.add('occupied');
        }

        seatingChart.appendChild(seatElement);
      });
    });
  }

  // Función para seleccionar o deseleccionar un asiento
  function selectSeat(rowIndex, seatIndex, seatElement) {
    if (!seatElement.classList.contains('occupied')) {
      if (seatElement.classList.contains('available')) {
        seatElement.classList.remove('available');
        seatElement.classList.add('selected');
        selectedSeats.push({ row: rowIndex, seat: seatIndex });
      } else if (seatElement.classList.contains('selected')) {
        seatElement.classList.remove('selected');
        seatElement.classList.add('available');
        selectedSeats = selectedSeats.filter(seat => seat.row !== rowIndex || seat.seat !== seatIndex);
      }
    }
    confirmBtn.disabled = selectedSeats.length === 0;
  }

  // Confirmar la selección
  confirmBtn.addEventListener('click', () => {
    if (selectedSeats.length > 0) {
      alert(`Has seleccionado los asientos: ${selectedSeats.map(seat => `Fila ${seat.row + 1} Asiento ${seat.seat + 1}`).join(', ')}`);

      // Actualizar el arreglo de asientos para reflejar la ocupación
      selectedSeats.forEach(seat => {
        seats[seat.row][seat.seat] = 0; // Marcar asiento como ocupado
      });

      // Recrear la interfaz con los asientos actualizados
      seatingChart.innerHTML = ''; // Limpiar la grilla
      createSeats(seats); // Crear los asientos actualizados
    }
  });
});


const urlParams = new URLSearchParams(window.location.search);
const movieId = urlParams.get('movieId');

let movieData;

async function fechtShowtimes(movieId) {
  try {
    const response = await fetch(`https://localhost:7276/api/Movies/GetMovieById?id=${movieId}`);
    movieData = await response.json();
    console.log(movieData);

    document.getElementById('movieTitle').textContent = movieData.title;
    document.getElementById('movieGenre').textContent = movieData.genre.description;
    document.getElementById('movieProducer').textContent = movieData.producer.company;
    document.getElementById('movieDuration').textContent = movieData.duration;

    // Populate the showtime selector
    const showtimeSelector = document.getElementById('showtimeSelector');
    movieData.showtimes.forEach(showtime => {
      const option = document.createElement('option');
      option.value = showtime.showtimeId;
      option.textContent = `Sala ${showtime.screenId} - ${new Date(showtime.startDate).toLocaleString()} to ${new Date(showtime.endDate).toLocaleString()}`;
      showtimeSelector.appendChild(option);
    });
    return movieData;
  } catch (error) {
    console.error('Error al obtener los showtimes:', error);
  }
}

document.addEventListener("DOMContentLoaded", () => fechtShowtimes(movieId));


document.getElementById('confirm-btn').addEventListener('click', async function () {

  const userId = localStorage.getItem("userId");
  const token = localStorage.getItem("JWT-Token")
  const showtimeId = document.getElementById("showtimeSelector").value;
  const pricePerSeat = 1500;

  if (!showtimeId) {
    alert("Por favor, selecciona un horario");
    return;
  }

  const selectedShowtime = movieData.showtimes.find(showtime => showtime.showtimeId == showtimeId)

  console.log(selectedShowtime);

  if (!selectedShowtime) {
    alert("Error: no se encontró el showtime seleccionado.");
    return;
  }

  function formatDate(date) {
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0'); // Meses de 2 dígitos
    const day = String(date.getDate()).padStart(2, '0'); // Días de 2 dígitos
    return `${year}-${month}-${day}`;
  }

  const bookingRequest = {
    customerId: userId,
    bookingDate: formatDate(new Date(selectedShowtime.startDate))
  };

  const ticketRequest = selectedSeats.map(seat => {
    const seatNumber = `A${seat.row + 1}${seat.seat + 1}`;

    return {
      showtimeId: showtimeId,
      seatNumber: seatNumber,
      saleDate: formatDate(new Date()), // Fecha de la venta en formato yyyy-mm-dd
      price: pricePerSeat
    };
  });

  const requestData = {
    bookingRequest: bookingRequest,
    ticketRequest: ticketRequest
  }

  try {
    const response = await fetch('https://localhost:7276/api/Booking', {
      method: 'POST',
      headers: {
        "Content-Type": "application/json",
        "Authorization": `Bearer ${token}`
      },
      body: JSON.stringify(requestData)
    });

    const result = await response.json();

    if (response.ok) {
      alert("Reserva realizada con éxito.");
      // Aquí puedes realizar alguna acción después de una reserva exitosa, como redirigir o limpiar la selección.
    } else {
      alert("Error al realizar la reserva: " + result.message);
    }
  } catch (error) {
    console.error("Error en la solicitud:", error);
    alert("Hubo un error al realizar la reserva.");
  }
})