/** @type {import('tailwindcss').Config} */
export default {
  content: ["./src/**/*.{html,js}"],
  theme: {
    extend: {},
  },
  plugins: [require("daisyui")],
  daisyui: {
    themes: [
      {
        mytheme: {
          primary: "#FFCE00",
          secondary: "#f6d860",
          accent: "#37cdbe",
          neutral: "#3d4451",
          warning: "#FF9C9C",
          "base-100": "#ffffff",
          "base-200": "#f1f1fB",
          "base-300": "#E7E7FF",
        },
      },
      "dark",
      "cupcake",
    ],
  },
};
