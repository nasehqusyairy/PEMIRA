// Initialize Bootstrap tooltips
const tooltipElements = [...document.querySelectorAll('[data-bs-title]')].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl));

// Disable tooltips when sidebar is not minimized
tooltipElements.forEach(el => el.toggleEnabled());

// Toggle sidebar minimized
document.getElementById('sidebar-toggler').addEventListener('click', () => {
  document.body.classList.toggle('sidebar-minimized');
  tooltipElements.forEach(el => el.toggleEnabled());
});
const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]')
const tooltipList = [...tooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl))