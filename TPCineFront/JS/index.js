
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

function generateMovieCards(movies) {
    // let movieGrid = document.getElementById('premiere-list');
    // premiereList.forEach((movie, index) => {
    //     const movieCard = document.createElement('div');
    //     // movieCard.href = "#";
    //     movieCard.className = `carousel-item ${ index === 0 ? 'active' : ''} movie-card`;
    //     movieCard.onclick = () => openModal(movie.title, movie.description);

    //     movieCard.innerHTML = `
    //         <div class="col-md-3">
    //             <div class="card card-body movie-poster">
    //                 <img class="img-fluid" src="${movie.posterUrl}" alt="${movie.title}">
    //                 <div class="movie-info">
    //                     <span class="release-date">Estreno: ${movie.releaseDate}</span>
    //                 </div>
    //             </div>
    //             <h3>${movie.title}</h3>
    //         </div>
    //     `;
    //     movieGrid.appendChild(movieCard);
    // });

    const movieGrid = document.getElementById('premiere-list');

    // Group the movies into sets of 4 for each carousel item
    for (let i = 0; i < movies.length; i += 4) {
        const carouselItem = document.createElement('div');
        carouselItem.className = `carousel-item ${i === 0 ? 'active' : ''} movie-card-group`;

        // Add 4 movies per carousel-item
        for (let j = i; j < i + 4 && j < movies.length; j++) {
            const movie = movies[j];
            const movieCard = document.createElement('div');
            movieCard.className = 'col-md-3 movie-card';
            movieCard.onclick = () => openModal(movie.title, movie.description);

            movieCard.innerHTML = `
                <div class="card card-body movie-poster" >
                    <img class="img-fluid" src="${movie.posterUrl}" alt="${movie.title}">
                    <div class="movie-info">
                        <span class="release-date">Estreno: ${movie.releaseDate}</span>
                    </div>
                </div>
                <h3>${movie.title}</h3>
            `;

            carouselItem.appendChild(movieCard);
        }

        movieGrid.appendChild(carouselItem);
    }
}

document.addEventListener("DOMContentLoaded", function () {
    flatpickr("#datePicker", {
        dateFormat: "Y-m-d",  // Set your preferred date format, e.g., "Y-m-d" for "YYYY-MM-DD"
        minDate: "today",     // Prevents selecting past dates
        defaultDate: "today", // Sets default date to today
        allowInput: true,     // Allows users to type in a date manually
        enableTime: true,
    });
});

//   document.addEventListener('DOMContentLoaded', generateMovieCards);

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
    // Group the movies into sets of 4 for each carousel item
    for (let i = 0; i < movies.length; i += 4) {
        const carouselItem = document.createElement('div');
        carouselItem.className = `carousel-item ${i === 0 ? 'active' : ''} movie-card-group`;

        // Add 4 movies per carousel-item
        for (let j = i; j < i + 4 && j < movies.length; j++) {
            const movie = movies[j];
            const movieCard = document.createElement('div');
            movieCard.className = 'col-md-3 movie-card';

            const releaseDate = new Date(movie.releaseDate).toLocaleDateString('es-ES', {
                day: '2-digit',
                month: '2-digit',
                year: 'numeric'
            });

            movieCard.innerHTML = `
                <div class="card card-body movie-poster" >
                    <img class="img-fluid" src="${movie.posterUrl}" alt="${movie.title}">
                    <div class="movie-info">
                        <span class="release-date">Disponible hasta: ${releaseDate}</span>
                    </div>
                </div>
                <h3>${movie.title}</h3>
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








    // const tbody = document.getElementById('componentes-body');
    // tbody.innerHTML = '';

    // movies.forEach(componente => {
    //     const row = document.createElement('tr');

    //     // Columna ID
    //     const movie_id = document.createElement('td');
    //     movie_id.textContent = componente.movie_id;
    //     row.appendChild(movie_id);

    //     // Columna Titulo
    //     const title = document.createElement('td');
    //     title.textContent = componente.title;
    //     row.appendChild(title);

    //     // Columna Relesa Date
    //     const relase_date = document.createElement('td');
    //     relase_date.textContent = componente.relase_date;
    //     row.appendChild(relase_date);

    //     // Columna Producer ID
    //     const producer_id = document.createElement('td');
    //     producer_id.textContent = componente.motivoBaja;
    //     row.appendChild(producer_id);


    // })




//   $('.carousel .carousel-item').each(function(){
//       var minPerSlide = 4;
//       var next = $(this).next();
//       if (!next.length) {
//       next = $(this).siblings(':first');
//       }
//       next.children(':first-child').clone().appendTo($(this));

//       for (var i=0;i<minPerSlide;i++) {
//           next=next.next();
//           if (!next.length) {
//               next = $(this).siblings(':first');
//               }

//           next.children(':first-child').clone().appendTo($(this));
//           }
//   });


window.addEventListener("DOMContentLoaded", () => {
    const userId = localStorage.getItem('userId');
    const token = localStorage.getItem('JWT-Token');

    if (userId && token) {
        document.getElementById("userIconLink").setAttribute('href', 'perfil.html')
    }
})