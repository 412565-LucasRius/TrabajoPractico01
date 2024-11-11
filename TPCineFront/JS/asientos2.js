document.addEventListener("DOMContentLoaded", async () => {
    const userId = localStorage.getItem("userId");
    const token = localStorage.getItem("JWT-Token");
  
    if (!userId || !token) {
      window.location.href = 'login.html';
    }
  
    let selectedSeats = [];
    const seatingChart = document.querySelector('.seating-chart');
    const confirmBtn = document.getElementById('confirm-btn');
  
    const urlParams = new URLSearchParams(window.location.search);
    const movieId = urlParams.get('movieId');
  
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
          option.textContent = `Sala ${showtime.screenId} - ${new Date(showtime.startDate).toLocaleString()} to ${new Date(showtime.endDate).toLocaleString()}`;
          showtimeSelector.appendChild(option);
        });
        return movieData;
      } catch (error) {
        console.error('Error al obtener los showtimes:', error);
      }
    }
  
    async function fetchOccupiedSeats(showtimeId) {
      try {
        const response = await fetch(`https://localhost:7276/api/Booking/BookedByShowtimeId/${showtimeId}`, {
          headers: { "Authorization": `Bearer ${token}` }
        });
        const occupiedSeats = await response.json();
        console.log("Asientos ocupados obtenidos de la API:", occupiedSeats); // Verificar los datos recibidos
        return occupiedSeats;
      } catch (error) {
        console.error('Error al obtener los asientos ocupados:', error);
      }
    }
  
    async function createSeats(seatsArray) {
      seatingChart.innerHTML = ''; // Limpiar el contenedor de asientos
  
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
  
    confirmBtn.addEventListener('click', async function () {
      const userId = localStorage.getItem("userId");
      const token = localStorage.getItem("JWT-Token");
      const showtimeId = document.getElementById("showtimeSelector").value;
      const pricePerSeat = 1500;
  
      if (!showtimeId) {
        alert("Por favor, selecciona un horario");
        return;
      }
  
      const selectedShowtime = movieData.showtimes.find(showtime => showtime.showtimeId == showtimeId);
  
      if (!selectedShowtime) {
        alert("Error: no se encontró el showtime seleccionado.");
        return;
      }
  
      function formatDate(date) {
        const year = date.getFullYear();
        const month = String(date.getMonth() + 1).padStart(2, '0');
        const day = String(date.getDate()).padStart(2, '0');
        return `${year}-${month}-${day}`;
      }
  
      const bookingRequest = {
        customerId: userId,
        bookingDate: formatDate(new Date(selectedShowtime.startDate))
      };
  
      const ticketRequest = selectedSeats.map(seat => {
        const seatNumber = String.fromCharCode(65 + seat.row) + String(seat.seat + 1).padStart(2, '0');
        return {
          showtimeId: showtimeId,
          seatNumber: seatNumber,
          saleDate: formatDate(new Date()),
          price: pricePerSeat
        };
      });
  
      const requestData = {
        bookingRequest: bookingRequest,
        ticketRequest: ticketRequest
      };
  
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
          selectedSeats = [];
          createSeats([[1, 1, 1, 1, 1], [1, 1, 1, 1, 1], [1, 1, 1, 1, 1], [1, 1, 1, 1, 1], [1, 1, 1, 1, 1]]);
        } else {
          alert("Error al realizar la reserva: " + result.message);
        }
      } catch (error) {
        console.error("Error en la solicitud:", error);
        alert("Hubo un error al realizar la reserva.");
      }
    });
  
    await fetchShowtimes(movieId);
    document.getElementById('showtimeSelector').addEventListener('change', () => {
      createSeats([[1, 1, 1, 1, 1], [1, 1, 1, 1, 1], [1, 1, 1, 1, 1], [1, 1, 1, 1, 1], [1, 1, 1, 1, 1]]);
    });
  });
  