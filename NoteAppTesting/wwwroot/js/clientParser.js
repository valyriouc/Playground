
document.addEventListener("keypress", (event) => {
    console.log(event.key);
    if (event.key == "Enter") {
        lostFocus();
    }
});

function getFocus(event) {
    const content = event.target.innerText;
    const input = document.createElement("input");
    input.value = content;
    const parent = event.target.parentElement;
    parent.replaceChild(input, event.target);
}

function lostFocus() {
    const content = document.querySelector("#title");
    const heading = convertToHeadingOne(content.value);
    const parent = content.parentElement;
    parent.replaceChild(heading, content);
    heading.onclick = getFocus;
}

function convertToHeadingOne(content) {
    const heading = document.createElement("h1");
    heading.innerText = content;
    return heading;
}