const background = document.querySelector("#site-background");
const bttnEnter = document.querySelector("#bttn-enter");
const main = document.querySelector(".main");
const mainBody = document.querySelector(".main-body");
const productBlocks = document.querySelectorAll(".product-block");
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
  const container = createElement("div", null, null, "popup-window", body);
  const img = createElement("img", null, null, "popup-img", container);

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
