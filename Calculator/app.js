let buttons = document.querySelectorAll("button:not(#heart)");
let numbersOnScreen = 0;

buttons.forEach(function (button) {
  button.addEventListener("click", buttonClickHandler);
});

document
  .querySelector("#deleteAll-button")
  .addEventListener("click", function () {
    for (var i = 0; i < buttons.length; i++) {
      buttons[i].disabled = false;
    }

    let deleteButton = document.querySelector("#deleteAll-button");
    deleteButton.classList.remove("blob");
  });

function buttonClickHandler() {
  const display = document.querySelector(".displayNumber");
  numbersOnScreen = display.textContent.length;

  if (numbersOnScreen === 11) {
    display.style.fontSize = "55px";
  }

  if (numbersOnScreen === 13) {
    display.style.fontSize = "50px";
  }

  if (numbersOnScreen === 15) {
    display.style.fontSize = "40px";
  }

  if (numbersOnScreen === 17) {
    buttons.forEach((bttn) => {
      if (
        bttn.classList.contains("number-button") ||
        bttn.classList.contains("purple-button") ||
        bttn.id === "point" ||
        bttn.id === "plus-minus"
      ) {
        bttn.disabled = true;
      }
    });

    document.querySelector("#deleteAll-button").classList.add("blob");
  }
}

function appendValue(value) {
  const display = document.querySelector(".display");
  const divWithNumbers = display.children[0];

  //Making first span always needed
  if (divWithNumbers.childElementCount === 0) {
    if (!Number.isInteger(Number(value))) {
      return;
    }
    createNewNumber(divWithNumbers, document);
  }

  if (
    display.childElementCount === 2 &&
    (value !== "+" || value !== "-" || value !== "*" || value !== "/")
  ) {
    display.lastChild.remove();
    if (checkIfValueIsOperator(value)) {
      operatorsChecker(value, divWithNumbers);
      return;
    } else {
      deleteNumbersDisplay();
    }
    createNewNumber(divWithNumbers);
  }

  if (operatorsChecker(value, divWithNumbers)) {
    return;
  }

  //Changing the sign of the numbers
  if (value === "+/-") {
    const currentSpan =
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
  if (checkIfValueIsOperator(value)) {
    if (
      divWithNumbers.children[
        divWithNumbers.childElementCount - 1
      ].textContent.charAt(
        divWithNumbers.children[divWithNumbers.childElementCount - 1]
          .textContent.length - 1
      ) === "."
    ) {
      console.log("Can't finish number on dot");
      return true;
    }

    if (
      divWithNumbers.children[divWithNumbers.childElementCount - 1].textContent
        .length !== 0
    ) {
      createNewNumber(divWithNumbers);
      if (spanChecker(value, divWithNumbers)) {
        divWithNumbers.children[
          divWithNumbers.childElementCount - 1
        ].textContent += value;
      }
      createNewNumber(divWithNumbers);
    }
    return;
  }
}

function spanChecker(value, divWithNumbers) {
  let currentSpan =
    divWithNumbers.children[divWithNumbers.childElementCount - 1];

  //Making symbols to calculator
  if (currentSpan.length < 1 && checkIfValueIsOperator(value)) {
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
      checkIfValueIsOperator(previousSpan.textContent) &&
      checkIfValueIsOperator(value)
    ) {
      console.log("Check previous span (if he is only symbol)");
      return false;
    }
  }

  return true;
}

function checkIfValueIsOperator(value) {
  if (value === "+" || value === "-" || value === "*" || value === "/") {
    return true;
  }
  return false;
}

function createNewNumber(divWithNumbers) {
  const span = document.createElement("span");
  divWithNumbers.appendChild(span);
}

function deleteNumbersDisplay() {
  let numbersOnDisplay = Array.from(
    document.querySelectorAll(".displayNumber")
  );
  numbersOnDisplay.forEach((numberOnDisplay) => {
    numberOnDisplay.textContent = "";
  });

  numbersOnScreen = 0;
  document.querySelector(".displayNumber").style.fontSize = "65px";
}

function calculate() {
  const divWithNumbers = document.querySelector(".displayNumber");
  let allElements = Array.from(divWithNumbers.childNodes);
  let length = allElements.length;

  //We need one more if statement, because there is a problem with operators(creating too many spans)
  if (length <= 3 && allElements[allElements.length - 1].textContent === "") {
    return;
  }

  let expression = "";

  allElements.forEach((el) => {
    expression += `${el.textContent} `;
  });

  let result = eval(expression);

  result = formatNumber(result);

  displayResult(result, expression, divWithNumbers);

  numbersOnScreen = 0;
  document.querySelector(".displayNumber").style.fontSize = "65px";
}

function formatNumber(number) {
  if (Number.isInteger(number)) {
    return number; // Return the whole number as is
  } else {
    return number.toFixed(2); // Apply formatting for non-whole numbers
  }
}

function displayResult(result, history, divWithNumbers) {
  deleteNumbersDisplay();
  createNewNumber(divWithNumbers);

  const display = document.querySelector(".display");

  let resultDisplay = divWithNumbers.children[0];
  resultDisplay.textContent = result;

  let historyNumber = document.createElement("div");
  historyNumber.classList.add("history");
  historyNumber.classList.add("displayNumber");
  historyNumber.textContent = history;
  display.appendChild(historyNumber);

  lengthDesigner(result, history);
}

function lengthDesigner(result, history) {
  const historyOnDisplay = document.querySelector(".history");
  const resultOnDisplay = document.querySelector(".displayNumber > span");
  if (result.toString().length > 9) {
    resultOnDisplay.style.fontSize = "55px";
  }

  if (result.toString().length >= 14) {
    resultOnDisplay.style.fontSize = "45px";
    historyOnDisplay.style.fontSize = "30px";
  }

  if (result.toString().length > 16 && history.length > 16) {
    resultOnDisplay.style.fontSize = "40px";
    historyOnDisplay.style.fontSize = "25px";
  }

  if (result.toString().length > 18 && history.length > 18) {
    resultOnDisplay.style.fontSize = "30px";
    historyOnDisplay.style.fontSize = "20px";
  }
}
