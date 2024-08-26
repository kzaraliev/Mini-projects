import emailjs from "@emailjs/browser";

export const add = async (params) => {
  emailjs.init("qcyeQ2ud7IRr-5jxS");

  const serviceId = "service_1ku2u34";
  const templateIdToCustomer = "template_e9863s2";

  emailjs
    .send(serviceId, templateIdToCustomer, params)
    .then(() => {
      console.log("Email sent successfully");
    })
    .catch(() => {
      alert("Something went wrong :'(");
    });

  const templateIdToBoss = "template_7bmzsml";
  emailjs.send(serviceId, templateIdToBoss, params).then().catch();

  localStorage.clear();
};
