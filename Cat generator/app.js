function generateCat() {
  const cat = document.querySelector(".container");
  cat.classList.add("moving");

  const button = document.querySelector(".new-cat");
  button.classList.add("fade-out");
  button.style.display = "none";

  const catImg = document.querySelector(".cat-img");

  fetch("https://api.thecatapi.com/v1/images/search")
    .then((res) => {
      return res.json();
    })
    .then((data) => {
      image = data[0];
      catImg.src = image.url;
    });

  setTimeout(() => {
    document.querySelector(".img-container").style.display = "block";
  }, 1000);
}
