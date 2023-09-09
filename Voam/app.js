const background = document.querySelector("#site-background");
const bttnEnter = document.querySelector("#bttn-enter");
const main = document.querySelector(".main");

bttnEnter.addEventListener("click", enterSite);
main.style.display = "none";

function enterSite(e) {
  e.preventDefault();

  background.style.transition = "background-size 0.7s ease-in-out";
  background.style.backgroundSize = "5%";
  background.style.backgroundRepeat = "repeat";
  background.style.filter = "brightness(70%)";

  bttnEnter.innerHTML = "";
  
  main.style.display = "flex";
  main.classList.add("fadeIn");
}
