window.onload = function () {    
    if (JSON.parse(localStorage.getItem('cate') || 'null') === null) {
        localStorage.setItem('cate', JSON.stringify("food"));
    }
    
    let cate = JSON.parse(localStorage.getItem('cate'));
    document.querySelector(`.nav-link[code-name='${cate}']`).click();
};
