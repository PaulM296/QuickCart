/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{html,ts}",
  ],
  theme: {
    extend: {
      screens: {
        '3xl': '1880px',
      },
      maxWidth: {
        'screen-3xl': '1880px',
      },
    },
  },
  plugins: [],
  important: true,
}