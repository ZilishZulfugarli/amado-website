var search = document.querySelector('.search');
var searchResult = document.querySelector('.search-result')

let cachedInput;

search.addEventListener('keyup', (e) => {
    const value = e.target.value.trim();

    if (value.length < 3) {
        searchResult.innerHTML = "";
        return;
    }

    if (cachedInput == value) return;

    cachedInput = value;

    fetch(`https://localhost:7231/shop/search?name=${value}`)
        .then(x => x.text())
        .then(x => searchResult.innerHTML = x);
});