// Initialize Bootstrap tooltips
const tooltipElements = [...document.querySelectorAll('[data-bs-title]')].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl));

// Disable tooltips when sidebar is not minimized
tooltipElements.forEach(el => el.toggleEnabled());

// Toggle sidebar minimized
document.getElementById('sidebar-toggler').addEventListener('click', () => {
  document.body.classList.toggle('sidebar-minimized');
  tooltipElements.forEach(el => el.toggleEnabled());
});
const tooltipTriggerList = document.querySelectorAll('[data-bs-title]')
const tooltipList = [...tooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl))

const copyText = (evt) => {
  evt.preventDefault()
  navigator.clipboard.writeText(evt.target.textContent)
  const el = tooltipList.filter(el => el._element == evt.target)[0]
  el.setContent({ '.tooltip-inner': 'Berhasil disalin!' })
  setTimeout(() => {
    el.setContent({ '.tooltip-inner': 'Salin' })
  }, 1000)
}