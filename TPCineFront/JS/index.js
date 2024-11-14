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

    document.getElementById("modalBookLink").href = "asientos.html?movieId=" + movie.movieId +"&isUpdate=False";
}

function closeModal() {
    const modal = document.getElementById("myModal");
    if (!modal) return;
    
    modal.style.display = "none";
    document.body.classList.remove('modal-open');
}

function handleOutsideClick(event) {
    const modal = document.getElementById("myModal");
    if (!modal) return;
    
    if (event.target === modal) {
        closeModal();
    }
}

window.addEventListener('click', handleOutsideClick);

document.addEventListener('keydown', function(event) {
    if (event.key === 'Escape') {
        closeModal();
    }
});


async function fetchMovies() {
    try {
        const response = await fetch('https://localhost:7276/api/Movies/GetAllPremiere');
        const movies = await response.json();

        loadMovies(movies);
    } catch (error) {
        console.error('Error al obtener las peliculas:', error);
    }
}


function loadMovies(movies) {
    const movieGrid = document.getElementById('premiere-list');

    
    for (let i = 0; i < movies.length; i += 4) {
        const carouselItem = document.createElement('div');
        carouselItem.className = `carousel-item ${i === 0 ? 'active' : ''} movie-card-group`;

        
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
                <a href="asientos.html?movieId=${movie.movieId}&isUpdate=False">
                    <button class="btn btn-primary" onclick="${CheckUserSession()}">Reservar</button>                    
                </a>
            `;

            carouselItem.appendChild(movieCard);
        }

        movieGrid.appendChild(carouselItem);
    }
}

document.addEventListener('DOMContentLoaded', fetchMovies);


async function CheckUserSession() {
    const userId = localStorage.getItem('userId');
    const token = localStorage.getItem('JWT-Token');


    if (!userId || !token) {
        window.location.href = 'login.html';
    } else {
        document.getElementById("userIconLink").setAttribute('href', 'perfil.html');
    }
}

