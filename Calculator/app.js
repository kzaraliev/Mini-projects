let buttons = document.querySelectorAll("button:not(#heart)");
let clickCount = 0;

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
  //clickCount++;
  const display = document.querySelector(".displayNumber");
  clickCount = display.textContent.length;

  if (clickCount === 11) {
    document.querySelector(".displayNumber").style.fontSize = "55px";
  }

  if (clickCount === 13) {
    document.querySelector(".displayNumber").style.fontSize = "50px";
  }

  if (clickCount === 15) {
    document.querySelector(".displayNumber").style.fontSize = "40px";
  }

  if (clickCount === 17) {
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

    let deleteButton = document.querySelector("#deleteAll-button");
    deleteButton.classList.add("blob");
  }
}

function appendValue(value) {
  const display = document.querySelector(".display");
  const divWithNumbers = display.children[0];

  //Making first span always needed
  if (divWithNumbers.childElementCount === 0) {
    if (
      value === "+" ||
      value === "-" ||
      value === "*" ||
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
    createNewNumber(divWithNumbers);
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

function createNewNumber(divWithNumbers) {
  let span = document.createElement("span");
  divWithNumbers.appendChild(span);
  //clickCount++;
}

function deleteNumbersDisplay() {
  let numbersOnDisplay = Array.from(
    document.querySelectorAll(".displayNumber")
  );
  numbersOnDisplay.forEach((numberOnDisplay) => {
    numberOnDisplay.textContent = "";
  });

  clickCount = 0;
  document.querySelector(".displayNumber").style.fontSize = "65px";
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

  result = formatNumber(result);

  displayResult(result, expression, divWithNumbers);

  clickCount = 0;
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
  if (result.toString().length > 9) {
    document.querySelector(".displayNumber > span").style.fontSize = "55px";
  }

  if (result.toString().length >= 14) {
    document.querySelector(".displayNumber > span").style.fontSize = "45px";
    document.querySelector(".history").style.fontSize = "30px";
  }

  if (result.toString().length > 16 && history.length > 16) {
    document.querySelector(".displayNumber > span").style.fontSize = "40px";
    document.querySelector(".history").style.fontSize = "25px";
  }

  if (result.toString().length > 18 && history.length > 18) {
    console.log(history.length);
    document.querySelector(".displayNumber > span").style.fontSize = "30px";
    document.querySelector(".history").style.fontSize = "20px";
  }
}
