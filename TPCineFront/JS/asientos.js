document.addEventListener("DOMContentLoaded", async () => {
  const userId = localStorage.getItem("userId");
  const token = localStorage.getItem("JWT-Token");

  if (!userId || !token) {
    window.location.href = 'login.html';
  }

  let selectedSeats = [];
  const seatingChart = document.querySelector('.seating-chart');
  const confirmBtn = document.getElementById('confirm-btn');
  const cancelBtn = document.getElementById('cancel-btn');

  const urlParams = new URLSearchParams(window.location.search);
  const movieId = urlParams.get('movieId');
  const bookingId = urlParams.get('bookingId');
  const isUpdate = urlParams.get('isUpdate');

  let movieData;

  async function fetchShowtimes(movieId) {
    try {
      const response = await fetch(`https://localhost:7276/api/Movies/GetMovieById?id=${movieId}`);
      movieData = await response.json();

      document.getElementById('movieTitle').textContent = movieData.title;
      document.getElementById('movieGenre').textContent = movieData.genre.description;
      document.getElementById('movieProducer').textContent = movieData.producer.company;
      document.getElementById('movieDuration').textContent = movieData.duration;

      // Populate the showtime selector
      const showtimeSelector = document.getElementById('showtimeSelector');
      movieData.showtimes.forEach(showtime => {
        const option = document.createElement('option');
        option.value = showtime.showtimeId;
        option.textContent = `Sala ${showtime.screenId} - ${new Date(showtime.movieTime).toLocaleString()}`;
        showtimeSelector.appendChild(option);
      });
      showtimeSelector.selectedIndex = 0;
      return movieData;
    } catch (error) {
      console.error('Error al obtener los showtimes:', error);
    }
  }

  async function fetchOccupiedSeats(showtimeId) {
    try {
        let response;
        const bookingId = urlParams.get('bookingId');
        console.log('isUpdate:', isUpdate);
        console.log('bookingId:', bookingId);
        console.log('showtimeId:', showtimeId);

        if (isUpdate === 'True'){
            console.log("Executing update path");
            response = await fetch(
                `https://localhost:7276/api/Booking/BookedByShowtimeIdandBooking/${showtimeId}/${bookingId}`,
                {
                    headers: { "Authorization": `Bearer ${token}` }
                }
            );
            console.log('Update path response:', response);
        } else {
            response = await fetch(
                `https://localhost:7276/api/Booking/BookedByShowtimeId/${showtimeId}`,
                {
                    headers: { "Authorization": `Bearer ${token}` }
                }
            );
        }

        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        const occupiedSeats = await response.json();
        console.log("Asientos ocupados obtenidos de la API:", occupiedSeats);
        return occupiedSeats;

    } catch (error) {
        console.error('Error al obtener los asientos ocupados:', error);
        throw error;
    }
}
  async function createSeats(seatsArray) {
    seatingChart.innerHTML = '';

    const showtimeId = document.getElementById("showtimeSelector").value;
    const occupiedSeats = await fetchOccupiedSeats(showtimeId) || [];

    seatsArray.forEach((row, rowIndex) => {
      row.forEach((seat, seatIndex) => {
        const seatElement = document.createElement('div');
        seatElement.classList.add('seat');

        const seatCode = String.fromCharCode(65 + rowIndex) + String(seatIndex + 1).padStart(2, '0');

        if (occupiedSeats.includes(seatCode)) {
          seatElement.classList.add('occupied');
        } else {
          seatElement.classList.add('available');
          seatElement.addEventListener('click', () => selectSeat(rowIndex, seatIndex, seatElement));
        }

        seatingChart.appendChild(seatElement);
      });
    });
  }

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

  confirmBtn.addEventListener('click', async function () {
    const userId = localStorage.getItem("userId");
    const token = localStorage.getItem("JWT-Token");
    const showtimeId = document.getElementById("showtimeSelector").value;
    

    if (!token) {
        alert("No hay sesión activa. Por favor inicia sesión.");
        return;
    }

    if (!showtimeId) {
        alert("Por favor, selecciona un horario");
        return;
    }

    if (!selectedSeats || selectedSeats.length === 0) {
        alert("Por favor, selecciona al menos un asiento");
        return;
    }

    const selectedShowtime = movieData.showtimes.find(showtime => showtime.showtimeId == showtimeId);

    if (!selectedShowtime) {
        alert("Error: no se encontró el showtime seleccionado.");
        return;
    }

    try {
        const ticketRequest = selectedSeats.map(seat => {
            const seatNumber = String.fromCharCode(65 + seat.row) + String(seat.seat + 1).padStart(2, '0');
            return {
                showtimeId: showtimeId,
                seatNumber: seatNumber,
                saleDate: new Date().toISOString()
            };
        });

        let response;
        let result;

        if (isUpdate === 'True') {
            response = await fetch(`https://localhost:7276/api/Booking/UpdateBooking?bookingId=${bookingId}`, {
                method: 'PUT',
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": `Bearer ${token}`
                },
                body: JSON.stringify(ticketRequest)
            });
        } else {
            const bookingRequest = {
                customerId: userId,
                bookingDate: new Date(selectedShowtime.movieTime).toISOString()
            };          
            const requestData = {
                bookingRequest: bookingRequest,
                ticketRequest: ticketRequest
            };

            response = await fetch('https://localhost:7276/api/Booking', {
                method: 'POST',
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": `Bearer ${token}`
                },
                body: JSON.stringify(requestData)
            });
        }

        if (!response.ok) {
            const errorText = await response.text();
            throw new Error(`HTTP error! status: ${response.status}, message: ${errorText}`);
        }

        result = await response.json();
        alert("Reserva realizada con éxito.");
        selectedSeats = [];
        createSeats([[1, 1, 1, 1, 1], [1, 1, 1, 1, 1], [1, 1, 1, 1, 1], [1, 1, 1, 1, 1], [1, 1, 1, 1, 1]]);
        window.location.href = 'index.html';

    } catch (error) {
        console.error('Error en la operación de reserva:', error);
        alert('Ocurrió un error al procesar la reserva. Por favor, intente nuevamente.');
    }
});

  await fetchShowtimes(movieId);
  document.getElementById('showtimeSelector').addEventListener('change', () => {
    createSeats([[1, 1, 1, 1, 1], [1, 1, 1, 1, 1], [1, 1, 1, 1, 1], [1, 1, 1, 1, 1], [1, 1, 1, 1, 1]]);
  });

  cancelBtn.addEventListener('click', function () {
    window.location.href = 'index.html'; 
  });
  
});