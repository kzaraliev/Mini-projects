const background = document.querySelector("#site-background");
const bttnEnter = document.querySelector("#bttn-enter");
const main = document.querySelector(".main");
const mainBody = document.querySelector(".main-body");
const productBlocks = document.querySelectorAll(".open-popup");
const body = document.querySelector("body");

bttnEnter.addEventListener("click", enterSite);

productBlocks.forEach((product) => {
  product.addEventListener("click", function () {
    showProduct(product);
  });
});

main.style.display = "none";

let price = 0;

function enterSite(e) {
  e.preventDefault();

  background.style.transition = "background-size 0.7s ease-in-out";
  background.style.backgroundSize = "5%";
  background.style.filter = "brightness(70%)";

  bttnEnter.innerHTML = "";

  main.style.display = "flex";
  main.classList.add("fadeIn");
}

function showProduct(product) {
  //access to product-block
  while (product.classList[0] != "product-block") {
    product = product.parentElement;
  }

  const container = createElement("div", null, null, "popup-window", body);
  const slideshow = product.firstElementChild.cloneNode(true);
  container.appendChild(slideshow);

  const contentContainer = createElement(
    "div",
    null,
    null,
    "popup-content",
    container
  );

  for (const child of product.children) {
    if (child.tagName == "IMG") {
      img.src = child.src;
    }

    if (child.tagName == "H1") {
      createElement("h1", child.textContent, null, null, contentContainer);
    }

    if (child.tagName == "H2") {
      price = Number(child.textContent.split("lv.")[0]);
      const totalPrice = createElement(
        "h2",
        child.textContent,
        null,
        "price",
        contentContainer
      );
    }
  }

  const orderContainer = createElement(
    "div",
    null,
    null,
    "popup-order",
    contentContainer
  );
  const amountSelector = createElement(
    "input",
    null,
    null,
    "quantity",
    orderContainer
  );

  const sizeSelector = createElement(
    "select",
    null,
    null,
    "size",
    orderContainer
  );

  const sSize = createElement("option", "S", null, null, sizeSelector);
  const mSize = createElement("option", "M", null, null, sizeSelector);
  const lSize = createElement("option", "L", null, null, sizeSelector);

  sSize.value = "S";
  mSize.value = "M";
  lSize.value = "L";

  amountSelector.type = "number";
  amountSelector.min = "1";

  const addToCartBttn = createElement(
    "button",
    "Add to cart",
    null,
    "add-to-Cart",
    orderContainer
  );

  const closeBttn = createElement("button", null, null, "closeBttn", container);
  closeBttn.innerHTML = `<i class="fa-solid fa-xmark"></i>`;
  closeBttn.addEventListener("click", closePopup);

  mainBody.classList.add("disable-scroll");
  main.classList.add("open-window");
  container.classList.add("fadeIn");

  function closePopup() {
    main.classList.remove("fadeIn");
    mainBody.classList.remove("disable-scroll");
    main.classList.remove("open-window");
    container.remove();
  }
}

function createElement(type, textContent, classes, id, parent) {
  const element = document.createElement(type);

  if (textContent) {
    element.textContent = textContent;
  }

  if (classes && classes.length > 0) {
    element.classList.add(...classes);
  }

  if (id) {
    element.setAttribute("id", id);
  }

  if (parent) {
    parent.appendChild(element);
  }

  return element;
}

//slideshow===============================
(function () {
  init();

  function init() {
    parents = document.getElementsByClassName("slideshow-container");

    for (j = 0; j < parents.length; j++) {
      var slides = parents[j].getElementsByClassName("mySlides");
      slides[0].classList.add("active-slide");
    }
  }

  links = document.querySelectorAll(".slideshow-container a");

  for (i = 0; i < links.length; i++) {
    links[i].onclick = function () {
      current = this.parentNode;

      var slides = current.getElementsByClassName("mySlides");
      curr_slide = current.getElementsByClassName("active-slide")[0];
      curr_slide.classList.remove("active-slide");
      if (this.className == "next") {
        if (curr_slide.nextElementSibling.classList.contains("mySlides")) {
          curr_slide.nextElementSibling.classList.add("active-slide");
        } else {
          slides[0].classList.add("active-slide");
        }
      }

      if (this.className == "prev") {
        if (curr_slide.previousElementSibling) {
          curr_slide.previousElementSibling.classList.add("active-slide");
        } else {
          slides[slides.length - 1].classList.add("active-slide");
        }
      }
    };
  }
})();
