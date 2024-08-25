/** @type {import('tailwindcss').Config} */

const colors = require('tailwindcss/colors');

export default {
  content: [
    "./src/App.vue",
    "./src/**/*.{html,js,vue,ts}"
  ],
  theme: {
    extend: {},
    colors: {
      ...colors,
    }
  },
  plugins: [],
}

