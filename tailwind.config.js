/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ['./wwwroot/js/*.js', 'Pages/**/*.cshtml', 'Pages/*.cshtml'],
  theme: {
      extend: {},
      container: {
          center: true,
      }
    },
    fontFamily: {
        sans: ['Graphik', 'sans-serif'],
        serif: ['Merriweather', 'serif'],
    },

  plugins: [],
}
