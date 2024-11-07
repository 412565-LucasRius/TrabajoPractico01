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
        console.log(movies);

        loadMovies(movies);
    } catch (error) {
        console.error('Error al obtener las peliculas:', error);
    }
}

// Cargar las películas en el carrusel
function loadMovies(movies) {
    const movieGrid = document.getElementById('premiere-list');
    for (let i = 0; i < movies.length; i += 4) {
        const carouselItem = document.createElement('div');
        carouselItem.className = `carousel-item ${i === 0 ? 'active' : ''} movie-card-group`;

        for (let j = i; j < i + 4 && j < movies.length; j++) {
            const movie = movies[j];
            const movieCard = document.createElement('div');
            movieCard.className = 'col-md-3 movie-card';
            movieCard.onclick = () => openModal(movie.title, movie.description, movie.movieId);

            const releaseDate = new Date(movie.releaseDate).toLocaleDateString('es-ES', {
                day: '2-digit',
                month: '2-digit',
                year: 'numeric'
            });

            movieCard.innerHTML = `
                <div class="card card-body movie-poster">
                    <img class="img-fluid" src="${movie.posterUrl}" alt="${movie.title}">
                    <div class="movie-info">
                        <span class="release-date">Disponible hasta: ${releaseDate}</span>
                    </div>
                </div>
                <h3>${movie.title}</h3>
                <a href="asientos.html?movieId=${movie.movieId}" class="btn btn-primary">Seleccionar Asientos</a>
            `;

            carouselItem.appendChild(movieCard);
        }

        movieGrid.appendChild(carouselItem);
    }
}

document.addEventListener('DOMContentLoaded', fetchMovies);

// Cargar los cines
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
    select.innerHTML = '';

    const opcionPredeterminada = document.createElement('option');
    opcionPredeterminada.value = '';
    opcionPredeterminada.disabled = true;
    opcionPredeterminada.selected = true;
    opcionPredeterminada.textContent = 'Seleccionar Cine';
    select.appendChild(opcionPredeterminada);

    cinemas.forEach(cinema => {
        const option = document.createElement('option');
        option.value = cinema.cinemaId;
        option.textContent = cinema.name;
        select.appendChild(option);
    });
}

document.addEventListener('DOMContentLoaded', fetchCinemas);

// Comprobación de sesión de usuario
window.addEventListener("DOMContentLoaded", () => {
    const userId = localStorage.getItem('userId');
    const token = localStorage.getItem('JWT-Token');

    if (!userId || !token) {
        window.location.href = 'login.html'; // Redirigir a la página de login si no hay sesión
    } else {
        document.getElementById("userIconLink").setAttribute('href', 'perfil.html');
    }
});
