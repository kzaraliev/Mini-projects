const qustionDataBase = [
  {
    question: "Quelle est la capitale de la France?",
    option1: "Marseille",
    option2: "Lyon",
    option3: "Paris",
    option4: "Bordeaux",
    ans: "answer3",
  },
  {
    question: "Quel est le plus long fleuve de France?",
    option1: "Seine",
    option2: "Loire",
    option3: "Rhône",
    option4: "Garonne",
    ans: "answer2",
  },
  {
    question: "Quel est l'oiseau national de la France?",
    option1: "Aigle",
    option2: "Cygne",
    option3: "Coq",
    option4: "Hirondelle",
    ans: "answer3",
  },
  {
    question: "Quelle était la monnaie officielle de la France avant l'euro?",
    option1: "Franc français",
    option2: "Livre sterling",
    option3: "Peseta",
    option4: "Lira",
    ans: "answer1",
  },
  {
    question:
      "Quelle ville est célèbre pour son festival de cinéma, qui a lieu chaque année?",
    option1: "Nice",
    option2: "Cannes",
    option3: "Monaco",
    option4: "Toulouse",
    ans: "answer2",
  },
  {
    question:
      "Comment s'appelle la fête nationale de la France, célébrée le 14 juillet?",
    option1: "Jour de l'Indépendance",
    option2: "Fête de la Bastille",
    option3: "Jour de la Victoire",
    option4: "Jour de la Paix.",
    ans: "answer2",
  },
  {
    question: 'Quel roi français a été surnommé "Le Roi Soleil"?',
    option1: "Louis XIII",
    option2: "Charles X",
    option3: "Louis XV",
    option4: "Louis XIV",
    ans: "answer4",
  },
  {
    question: "Quelle est la montagne la plus célèbre de France?",
    option1: "Mont Blanc",
    option2: "Pyrénées",
    option3: "Vésuve",
    option4: "Etna",
    ans: "answer1",
  },
  {
    question:
      "Quel est le plus ancien pont de Paris encore debout aujourd'hui?",
    option1: "Pont des Arts",
    option2: "Pont de la Concorde",
    option3: "Pont Alexandre III",
    option4: "Pont Neuf",
    ans: "answer4",
  },
  {
    question:
      "Quelle bataille célèbre a marqué la fin des guerres napoléoniennes en 1815?",
    option1: "Bataille de Leipzig",
    option2: "Bataille de Waterloo",
    option3: "Bataille de Trafalgar",
    option4: "Bataille d'Austerlitz",
    ans: "answer2",
  },
  {
    question:
      'Quel célèbre écrivain français est l\'auteur de "Les Misérables"?',
    option1: "Victor Hugo",
    option2: "Émile Zola",
    option3: "Gustave Flaubert",
    option4: "Honoré de Balzac",
    ans: "answer1",
  },
  {
    question:
      'Quel célèbre artiste français est associé au mouvement impressionniste et a peint "Impression, soleil levant"?',
    option1: "Claude Monet",
    option2: "Édouard Manet",
    option3: "Pierre-Auguste Renoir",
    option4: "Paul Cézanne",
    ans: "answer1",
  },
  {
    question:
      "Quel philosophe français est considéré comme le père de la philosophie moderne?",
    option1: "Jean-Paul Sartre",
    option2: "Voltaire",
    option3: "René Descartes",
    option4: "Blaise Pascal",
    ans: "answer3",
  },
  {
    question:
      "Quel est le nom du palais où réside le Président de la République française?",
    option1: "Palais du Luxembourg",
    option2: "Palais Bourbon",
    option3: "Palais Royal",
    option4: "Palais de l'Élysée",
    ans: "answer4",
  },
  {
    question:
      "Quel est le type de gouvernement de la France?",
    option1: "Monarchie constitutionnelleRe",
    option2: "République fédérative",
    option3: "République présidentielle",
    option4: "Monarchie absolue",
    ans: "answer3",
  },
];

// getting referrence
const questionContainer = document.querySelector("h2");
const option1 = document.querySelector("#option1");
const option2 = document.querySelector("#option2");
const option3 = document.querySelector("#option3");
const option4 = document.querySelector("#option4");
const submitButton = document.querySelector("button");
const usersAnswer = document.querySelectorAll(".answer");
const scoreArea = document.querySelector("#ShowScore");

// This function is rendering all the texts

let questionCount = 0;
let score = 0;
const mainFunc = () => {
  const list = qustionDataBase[questionCount];
  questionContainer.innerText = list.question;
  option1.innerText = list.option1;
  option2.innerText = list.option2;
  option3.innerText = list.option3;
  option4.innerText = list.option4;
};

mainFunc();

// this function is for answer checking

const goCheckAnswer = () => {
  let answers;
  usersAnswer.forEach((data) => {
    if (data.checked) {
      answers = data.id;
    }
  });
  return answers;
};

const deselectAll = () => {
  usersAnswer.forEach((data) => {
    data.checked = false;
  });
};

submitButton.addEventListener("click", () => {
  const checkAnswer = goCheckAnswer();
  console.log(checkAnswer);

  if (checkAnswer === qustionDataBase[questionCount].ans) {
    score++;
  }
  questionCount++;
  deselectAll();
  if (questionCount < qustionDataBase.length) {
    mainFunc();
  } else {
    scoreArea.style.display = "block";
    scoreArea.innerHTML = `
      <h3>Your score is ${score} / ${qustionDataBase.length}</h3>
      <button class='btn' onclick='location.reload()'>Play Again</button>
      `;
    questionContainer.style.display = "none";
    submitButton.style.display = "none";
    document.querySelector("ul").style.display = "none";
  }
});
