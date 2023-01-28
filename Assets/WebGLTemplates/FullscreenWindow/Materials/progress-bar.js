function setLoaderProgressTo(value){
    const fill = document.querySelector("div.bar");
    const fillText = document.querySelector("p.counter");
    fill.animate(
        [
            {width: (value * 100) + "%"}
        ],
        {
            duration : 900,
            fill: "forwards"
        }
    );

    fillText.textContent = (value * 100).toFixed() + "%";
}

