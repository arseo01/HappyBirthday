let currentRotation = 0;

document.addEventListener("DOMContentLoaded", function() {
    const input = document.querySelector("#inputwish");
    const btn = document.querySelector("#makeit");
    
    input.addEventListener("input", validate);
    
    function validate() {
        if (input.value.trim() === "") {
            btn.setAttribute("disabled", "disabled");
        } else {
            btn.removeAttribute("disabled");
        }
    }
});

function rotateDiv() {
    currentRotation += 180;
    document.getElementById('top').style.transform = `rotateX(${currentRotation}deg) translateY(205px)`;
    document.getElementById("clickme").style.display = "none";

    // setTimeout(() => {
    //     document.getElementById("text").style.display = "block";
    //     document.getElementById("inputwish").style.display = "block";
    //     document.getElementById("makeit").style.display = "block";
    // }, 800);
}

async function changetext() {
    const input = document.querySelector("#inputwish");
    // console.log(input);
    await creatWish("arseo", input.value, 15);
    
    document.getElementById("text").innerHTML = "Hope your wish comes true";
}


async function creatWish(name, wish, mosha) {
    const url = `https://localhost:7294/api/Wish/addWish?name=${name}&wish=${wish}&mosha=${mosha}`;
    try {
      const response = await fetch(url, { method: "POST" });
      if (!response.ok) {
        throw new Error(`Response status: ${response.status}`);
      }
  
      const json = await response.json();
      console.log(json);
    } catch (error) {
      console.error(error.message);
    }
}