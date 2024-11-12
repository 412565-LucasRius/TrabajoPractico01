// Funciones para abrir y cerrar el modal
function openModal(movie) {
    const myModal = document.getElementById("myModal");
    myModal.style.display = "block";
    myModal.style.width = "80%";
    myModal.style.margin = "auto";
    myModal.style.position = "fixed";
    myModal.style.top = "50%";
    myModal.style.left = "50%";
    myModal.style.transform = "translate(-50%, -50%)";
    const title = document.getElementById('modalTitle');
    title.innerText = movie.title;
    
    const genre = document.getElementById('modalGenre');
    genre.innerHTML = `<strong>Género:</strong> ${movie.genre.description}`;
    
    const duration = document.getElementById('modalDuration');
    duration.innerHTML = `<strong>Duración:</strong> ${movie.duration} minutos`;
    
    const image = document.getElementById('modalImage');
    image.src = `assets/${movie.imageName}.jfif`;

    
    const clasification = document.getElementById('modalClasification');
    clasification.innerHTML = `<strong>Clasificación:</strong> ${movie.clasification.description}`;
    
    const producer = document.getElementById('modalProducer');
    producer.innerHTML = `<strong>Productor:</strong> ${movie.producer.company}`;

    const actionButton = document.getElementById('modalBookMovie');
    actionButton.onclick = CheckUserSession();

    document.getElementById("modalBookLink").href = "asientos.html?movieId=" + movie.movieId;
}

function closeModal() {
    document.getElementById("myModal").style.display = "none";
}

// Cerrar el modal si se hace clic fuera de él
window.onclick = function (event) {
    if (event.target == document.getElementById("myModal")) {
        closeModal();
    }
}

// Cargar las películas
async function fetchMovies() {
    try {
        const response = await fetch('https://localhost:7276/api/Movies/GetAllPremiere');
        const movies = await response.json();

        loadMovies(movies);
    } catch (error) {
        console.error('Error al obtener las peliculas:', error);
    }
}

// Cargar las películas en el carrusel
function loadMovies(movies) {
    const movieGrid = document.getElementById('premiere-list');

    // Agrupar las películas en conjuntos de 4 para cada elemento del carrusel
    for (let i = 0; i < movies.length; i += 4) {
        const carouselItem = document.createElement('div');
        carouselItem.className = `carousel-item ${i === 0 ? 'active' : ''} movie-card-group`;

        // Agregar 4 películas por elemento del carrusel
        for (let j = i; j < i + 4 && j < movies.length; j++) {
            const movie = movies[j];
            const movieCard = document.createElement('div');
            movieCard.className = 'col-md-3 movie-card';
            movieCard.onclick = () => openModal(movie);


            const releaseDate = new Date(movie.lastReleaseDate).toLocaleDateString('es-ES', {
                day: '2-digit',
                month: '2-digit',
                year: 'numeric'
            });

            movieCard.innerHTML = `
                <div class="card card-body movie-poster">
                   <img class="img-fluid" src="assets/${movie.imageName}.jfif" alt="${movie.title}">
                   <div class="movie-info">
                        <span class="lastReleaseDate" style="color: white; background-color: red; padding: 5px; border-radius: 5px;">Estreno: ${releaseDate} </span>
                    </div>
                </div>
                <div class="movie-details">
                <h3>${movie.title}</h3>
                <a href="asientos.html?movieId=${movie.movieId}">
                    <button class="btn btn-primary" onclick="${CheckUserSession()}">Reservar</button>                    
                </a>
            `;

            carouselItem.appendChild(movieCard);
        }

        movieGrid.appendChild(carouselItem);
    }
}

document.addEventListener('DOMContentLoaded', fetchMovies);

async function fetchCinemas() {
    try {
        const response = await fetch('https://localhost:7276/api/Cinema/GetAllCinemas');
        const cinemas = await response.json();
        loadCinemas(cinemas);
    } catch (error) {
        console.error('Error al obtener los Cines:', error);
    }
}

function loadCinemas(cinemas) {
    const select = document.getElementById("locationSelect");
    // Limpiar cualquier opción existente antes de agregar las nuevas
    select.innerHTML = '';

    // Agregar la opción predeterminada
    const opcionPredeterminada = document.createElement('option');
    opcionPredeterminada.value = '';
    opcionPredeterminada.disabled = true;
    opcionPredeterminada.selected = true;
    opcionPredeterminada.textContent = 'Seleccionar Cine';
    select.appendChild(opcionPredeterminada);

    // Agregar las opciones del arreglo
    cinemas.forEach(cinema => {
        const option = document.createElement('option');
        option.value = cinema.cinemaId;
        option.textContent = cinema.name;
        select.appendChild(option);
    });
}

document.addEventListener('DOMContentLoaded', fetchCinemas);


// Comprobación de sesión de usuario

async function CheckUserSession() {
    const userId = localStorage.getItem('userId');
    const token = localStorage.getItem('JWT-Token');


    if (!userId || !token) {
        window.location.href = 'login.html'; // Redirigir a la página de login si no hay sesión
    } else {
        document.getElementById("userIconLink").setAttribute('href', 'perfil.html');
    }
}
