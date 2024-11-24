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

const previewImage = (evt, elId) => {
  const reader = new FileReader()
  reader.onload = (e) => {
    document.getElementById(elId).src = e.target.result
  }
  reader.readAsDataURL(evt.target.files[0])
}

const showPassword = () => {
  const passwordInput = document.getElementById('Password');
  const showPasswordButton = document.getElementById('show-password');
  const icon = showPasswordButton.querySelector('i');
  if (passwordInput.type === 'password') {
    passwordInput.type = 'text';
    icon.classList.remove('bi-eye');
    icon.classList.add('bi-eye-slash');
    showPasswordButton.title = 'Sembunyikan Kata Sandi';
  } else {
    passwordInput.type = 'password';
    icon.classList.remove('bi-eye-slash');
    icon.classList.add('bi-eye');
    showPasswordButton.title = 'Tampilkan Kata Sandi';
  }
}