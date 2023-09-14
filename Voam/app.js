const background = document.querySelector("#site-background");
const bttnEnter = document.querySelector("#bttn-enter");
const main = document.querySelector(".main");
const mainBody = document.querySelector(".main-body");
const productBlocks = document.querySelectorAll(".open-popup");
const body = document.querySelector("body");
const shoppingCart = document.querySelector("#shopping-cart");

slideshowMove();

bttnEnter.addEventListener("click", enterSite);
shoppingCart.addEventListener("click", openShoppingCart);

productBlocks.forEach((product) => {
  product.addEventListener("click", function () {
    showProduct(product);
  });
});

main.style.display = "none";
let itemsInCart = [];

function enterSite(e) {
  e.preventDefault();

  background.style.transition = "background-size 0.7s ease-in-out";
  background.style.backgroundSize = "5%";
  background.style.filter = "brightness(70%)";

  bttnEnter.innerHTML = "";

  main.style.display = "flex";
  main.classList.add("fadeIn");
}

function createCloseButton(container) {
  const closeBttn = createElement(
    "div",
    null,
    ["close-container"],
    null,
    container
  );
  createElement("div", null, ["leftright"], null, closeBttn);
  createElement("div", null, ["rightleft"], null, closeBttn);

  closeBttn.addEventListener("click", function () {
    main.classList.remove("fadeIn");
    mainBody.classList.remove("disable-scroll");
    main.classList.remove("open-window");
    container.remove();
  });
}

function openShoppingCart() {
  const container = createElement("div", null, null, "popup-window", body);
  const itemsContainer = createElement(
    "div",
    null,
    null,
    "items-shopping-cart",
    container
  );

  const item = createElement(
    "div",
    null,
    null,
    "item-shopping-cart",
    itemsContainer
  );

  createCloseButton(container);
}

function showProduct(product) {
  //access to product-block
  while (product.classList[0] != "product-block") {
    product = product.parentElement;
  }

  const container = createElement("div", null, null, "popup-window", body);
  const slideshow = product.firstElementChild.cloneNode(true);
  slideshow.setAttribute("id", "popup-slideshow");
  container.appendChild(slideshow);

  const contentContainer = createElement(
    "div",
    null,
    null,
    "popup-content",
    container
  );

  const content = product.childNodes[3].cloneNode(true);
  content.classList.remove("product-content");
  content.setAttribute("id", "popup-content-container");
  contentContainer.appendChild(content);

  const sizeContainer = createElement(
    "div",
    null,
    null,
    "size-container",
    contentContainer
  );

  createElement("p", "Size", null, "size-text", sizeContainer);

  const sizeSelector = createElement(
    "select",
    null,
    null,
    "size",
    sizeContainer
  );

  const defaultText = createElement(
    "option",
    "Choose an option",
    null,
    null,
    sizeSelector
  );
  const sSize = createElement("option", "S", null, null, sizeSelector);
  const mSize = createElement("option", "M", null, null, sizeSelector);
  const lSize = createElement("option", "L", null, null, sizeSelector);

  sSize.value = "S";
  mSize.value = "M";
  lSize.value = "L";

  defaultText.value = "";
  defaultText.disabled = true;
  defaultText.selected = true;

  const orderContainer = createElement(
    "div",
    null,
    null,
    "popup-order",
    contentContainer
  );

  const amountContainer = createElement(
    "div",
    null,
    ["number"],
    null,
    orderContainer
  );
  createElement("span", "-", ["minus", "unselectable"], null, amountContainer);

  const amountSelector = createElement(
    "input",
    null,
    null,
    "quantity",
    amountContainer
  );

  createElement("span", "+", ["plus", "unselectable"], null, amountContainer);

  amountSelector.value = "1";
  amountSelector.type = "number";
  amountSelector.min = "1";

  const addToCartBttn = createElement(
    "button",
    null,
    ["addtocart"],
    null,
    orderContainer
  );

  const pretext = createElement("div", null, ["pretext"], null, addToCartBttn);
  pretext.innerHTML = '<i class="fas fa-cart-plus"></i> ADD TO CART';

  const pretextDone = createElement(
    "div",
    null,
    ["pretext", "done"],
    null,
    addToCartBttn
  );
  const postText = createElement("div", null, ["posttext"], null, pretextDone);
  postText.innerHTML = '<i class="fas fa-check"></i> ADDED';

  createCloseButton(container);

  mainBody.classList.add("disable-scroll");
  main.classList.add("open-window");
  container.classList.add("fadeIn");

  links = document.querySelectorAll(".slideshow-container a");

  slideshowMove();

  document.querySelector(".minus").addEventListener("click", function () {
    let input = document.querySelector("input");
    let count = Number(input.value) - 1;
    if (count < 1) {
      return;
    }
    input.value = count.toString();
    input.textContent = input.value;
  });
  document.querySelector(".plus").addEventListener("click", function () {
    let input = document.querySelector("input");
    let count = Number(input.value) + 1;
    input.value = count.toString();
    input.textContent = input.value;
  });

  const done = document.querySelector(".done");
  addToCartBttn.addEventListener("click", () => {

    if (sizeSelector.value == "") {
      sizeSelector.classList.add("errors");
      addToCartBttn.disabled = true;
      setTimeout(function () {
        sizeSelector.classList.remove("errors");
        addToCartBttn.disabled = false;
      }, 500);
      return;
    }
    done.style.transform = "translate(0px)";

    const itemForCart = {
      img: slideshow.childNodes[1].childNodes[3],
      productName: content.childNodes[3].textContent,
      price: content.childNodes[5].textContent.match(/(\d+)/)[0],
      size: sizeSelector.value,
      amount: amountSelector.value
    };

    itemsInCart.push(itemForCart);

    console.log(itemsInCart);

    setTimeout(function () {
      done.style.transform = "translate(-110%) skew(-40deg)";
    }, 1000);
  });
}

//slideshow===============================
function slideshowMove() {
  init();

  function init() {
    parents = document.querySelectorAll(
      ".slideshow-container:not(.initialized-container)"
    );

    for (j = 0; j < parents.length; j++) {
      var slides = parents[j].getElementsByClassName("mySlides");
      slides[0].classList.add("active-slide");
    }

    parents.forEach((parent) => {
      parent.classList.add("initialized-container");
    });
  }

  let links = document.querySelectorAll(".slideshow-container a");

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
