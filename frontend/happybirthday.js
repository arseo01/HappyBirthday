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

    setTimeout(() => {
        document.getElementById("text").style.display = "block";
        document.getElementById("inputwish").style.display = "block";
        document.getElementById("makeit").style.display = "block";
    }, 800);
}

async function changetext() {
    const Wish = document.querySelector("#inputwish");
    const inputName = document.querySelector("#inputname");
    const inputMosha = document.querySelector("#inputage");
    // console.log(input);

    const name = inputName.value;
    const Mosha = parseInt(inputMosha.value) || null; 
    await createWish(name, Wish, Mosha);
    
    document.getElementById("text").innerHTML = "I hope your wish comes true.";
}


async function createWish(name, wish, mosha) {
    const url = `https://localhost:7294/api/Wish/MakeAWish`;
    const wishData = {
        Name: name || "",
        Wish: wish,
        Mosha: parseInt(mosha) || null,
        Viti: new Date().getFullYear()
    };

    try {
        const response = await fetch(url, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(wishData)
        });

        if (!response.ok) {
            throw new Error(`Response status: ${response.status}`);
        }

        const json = await response.json();
        console.log(json);
    } catch (error) {
        console.error(error.message);
    }
}