fetch("https://picsum.photos/1920/?blur").then((photo) => {
  const url = photo.url;
  document.querySelector("body").style.backgroundImage = `url(${url})`;
});

document.querySelector(".container > h1").addEventListener("click", openQuote);


function openQuote() {
  fetch("http://api.quotable.io/random")
    .then((res) => res.json())
    .then((data) => {
      console.log(data);
      document.querySelector(".quote > p").innerHTML = data.content;
  
      const author = document.querySelector("figcaption > a");
      author.innerHTML = data.author;
      author.setAttribute("target", "_blank");
      author.setAttribute(
        "href",
        `https://www.google.com/search?q=${data.author}`
      );
    });
  const container = document.querySelector(".container");
  const content = document.querySelector(".content");

  container.classList.add("fadeIn");
  content.style.display = "block";
}
