// Funciones para abrir y cerrar el modal
function openModal(title, description) {
    document.getElementById("modalTitle").innerText = title;
    document.getElementById("modalDescription").innerText = description;
    document.getElementById("myModal").style.display = "block";
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
            movieCard.onclick = () => openModal(movie.title, movie.description, movie.movieId);


            movieCard.innerHTML = `
                <div class="card card-body movie-poster">
                    // <img class="img-fluid" src="assets/${movie.imageName}.jfif" alt="${movie.title}">
                   <div class="movie-info">
                        <span class="duration" style="color: white; background-color: red; padding: 5px; border-radius: 5px;">Duración: ${movie.duration} minutos</span>
                    </div>
                </div>
                <div class="movie-details">
                <h3>${movie.title}</h3>
                <a href="asientos.html?movieId=${movie.movieId}" class="btn btn-primary">
                    <button onclick="${CheckUserSession()}">Seleccionar Asientos</button>                    
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
