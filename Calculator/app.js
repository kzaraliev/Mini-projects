var buttons = document.querySelectorAll("button:not(#heart)");
var clickCount = 0;

function buttonClickHandler() {
  clickCount++;
  
  if (clickCount === 11) {
    document.querySelector(".displayNumber").style.fontSize = "55px";
  }
}

function removeEventListeners(buttons, eventListener) {
  buttons.forEach(function (button) {
    button.removeEventListener("click", eventListener);
  });
}

function addEventListeners(buttons, eventListener) {
  buttons.forEach(function (button) {
    button.addEventListener("click", eventListener);
  });
}

buttons.forEach(function (button) {
  button.addEventListener("click", buttonClickHandler);
});

function appendValue(value) {
  const display = document.querySelector(".display");
  const divWithNumbers = display.children[0];

  //Add function to display properly +/- (when number is positive => negative)+

  //Making first span always needed
  if (divWithNumbers.childElementCount === 0) {
    if (
      value === "+" ||
      value === "-" ||
      value === "x" ||
      value === "/" ||
      value === "%" ||
      value === "." ||
      value === "+/-"
    ) {
      return;
    }
    createNewNumber(divWithNumbers, document);
  }

  if (
    display.childElementCount === 2 &&
    (value !== "+" || value !== "-" || value !== "*" || value !== "/")
  ) {
    display.lastChild.remove();
    if (value === "+" || value === "-" || value === "*" || value === "/") {
      operatorsChecker(value, divWithNumbers);
      return;
    } else {
      deleteNumbersDisplay();
    }
    createNewNumber(divWithNumbers, document);
  }

  operatorsChecker(value, divWithNumbers);

  //Changing the sign of the numbers
  if (value === "+/-") {
    let currentSpan =
      divWithNumbers.children[divWithNumbers.childElementCount - 1];
    if (currentSpan.textContent.length > 0) {
      if (currentSpan.textContent.charAt(0) === "-") {
        currentSpan.textContent = currentSpan.textContent.substring(1);
      } else {
        currentSpan.textContent = "(-" + currentSpan.textContent + ")";
      }
    }
    return;
  }

  //Final checker for the current span
  if (spanChecker(value, divWithNumbers)) {
    divWithNumbers.children[divWithNumbers.childElementCount - 1].textContent +=
      value;
  }
}

function operatorsChecker(value, divWithNumbers) {
  if (value === "+" || value === "-" || value === "*" || value === "/") {
    if (
      divWithNumbers.children[
        divWithNumbers.childElementCount - 1
      ].textContent.charAt(
        divWithNumbers.children[divWithNumbers.childElementCount - 1]
          .textContent.length - 1
      ) === "."
    ) {
      console.log("Can't finish number on dot");
      return;
    }

    if (
      divWithNumbers.children[divWithNumbers.childElementCount - 1].textContent
        .length !== 0
    ) {
      createNewNumber(divWithNumbers, document);
      if (spanChecker(value, divWithNumbers)) {
        divWithNumbers.children[
          divWithNumbers.childElementCount - 1
        ].textContent += value;
      }
      createNewNumber(divWithNumbers, document);
    }
    return;
  }
}

function spanChecker(value, divWithNumbers) {
  let currentSpan =
    divWithNumbers.children[divWithNumbers.childElementCount - 1];

  //Making symbols to calculator
  if (
    currentSpan.length < 1 &&
    (value === "+" || value === "-" || value === "*" || value === "/")
  ) {
    return true;
  }

  //Check if
  if (currentSpan.textContent.length === 0 && value === ".") {
    console.log("Can't start number with dot");
    return false;
  }

  //Check if the span already have dot
  if (currentSpan.textContent.includes(".") && value === ".") {
    console.log("No more dots");
    return false;
  }

  //Check previous span (if he is only symbol)
  if (divWithNumbers.childElementCount > 1) {
    let previousSpan =
      divWithNumbers.children[divWithNumbers.childElementCount - 2];
    if (
      (previousSpan.textContent === "+" ||
        previousSpan.textContent === "-" ||
        previousSpan.textContent === "*" ||
        previousSpan.textContent === "/") &&
      (value === "+" || value === "-" || value === "*" || value === "/")
    ) {
      console.log("Check previous span (if he is only symbol)");
      return false;
    }
  }

  return true;
}

function createNewNumber(divWithNumbers, document) {
  let span = document.createElement("span");
  divWithNumbers.appendChild(span);
}

function deleteNumbersDisplay() {
  let numbersOnDisplay = Array.from(
    document.querySelectorAll(".displayNumber")
  );
  numbersOnDisplay.forEach((numberOnDisplay) => {
    numberOnDisplay.textContent = "";
  });

  removeEventListeners(buttons, buttonClickHandler);
  clickCount = 0;
  addEventListeners(buttons, buttonClickHandler);
}

function calculate() {
  const divWithNumbers = document.querySelector(".displayNumber");
  let allElements = Array.from(divWithNumbers.childNodes);
  let length = allElements.length;

  //We need one more if statement, because there is a problem with symbols(creating too many spans)
  if (length <= 3 && allElements[allElements.length - 1].textContent === "") {
    return;
  }

  let expression = "";

  allElements.forEach((el) => {
    expression += `${el.textContent} `;
  });

  let result = eval(expression);

  result = Math.round((result + Number.EPSILON) * 100) / 100

  displayResult(result, expression, divWithNumbers);

  removeEventListeners(buttons, buttonClickHandler);
  clickCount = 0;
  document.querySelector(".displayNumber").style.fontSize = "65px";
  addEventListeners(buttons, buttonClickHandler);
}

function displayResult(result, history, divWithNumbers) {

  deleteNumbersDisplay();
  createNewNumber(divWithNumbers, document);
  
  const display = document.querySelector(".display");
  
  let resultDisplay = divWithNumbers.children[0];
  resultDisplay.textContent = result;
  
  if (result.toString().length > 9) {
    document.querySelector(".displayNumber > span").style.fontSize = "55px";
  }

  let historyNumber = document.createElement("div");
  historyNumber.classList.add("history");
  historyNumber.classList.add("displayNumber");
  historyNumber.textContent = history;
  display.appendChild(historyNumber);
}
