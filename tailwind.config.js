/** @type {import('tailwindcss').Config} */
export default {
  content: [
    "./src/App.vue",
    "./src/**/*.{html,js,vue,ts}"
  ],
  theme: {
    extend: {},
    colors: {
      violetDark: '#4B49AC',
      violetLight: '#7978E9',
      blueLight: '#7DA0FA',
      redLight: '#F3797E',
      whiteDark: '#d3d3d3',
      whiteLight: '#FFFFFF',
      grayLight: '#757C8B',
      grayDark: '#232323',
      greenLight: '#74A67E',
      yellowWarning: '#F7CC7F',
    }
  },
  plugins: [],
}

