
function openModal(title, description) {
    document.getElementById("modalTitle").innerText = title;
    document.getElementById("modalDescription").innerText = description;
    document.getElementById("myModal").style.display = "block";
}

function closeModal() {
    document.getElementById("myModal").style.display = "none";
}

// Cerrar el modal si se hace clic fuera de él
window.onclick = function(event) {
    if (event.target == document.getElementById("myModal")) {
        closeModal();
    }
}

document.addEventListener("DOMContentLoaded", function() {
    flatpickr("#datePicker", {
      dateFormat: "Y-m-d",  // Set your preferred date format, e.g., "Y-m-d" for "YYYY-MM-DD"
      minDate: "today",     // Prevents selecting past dates
      defaultDate: "today", // Sets default date to today
      allowInput: true,     // Allows users to type in a date manually
      enableTime: true,
    });
  });