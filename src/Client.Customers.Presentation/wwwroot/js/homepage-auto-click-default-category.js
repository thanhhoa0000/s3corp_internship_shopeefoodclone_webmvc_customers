window.onload = function () {
    console.log(JSON.parse(localStorage.getItem('cate')));
    
    if (JSON.parse(localStorage.getItem('cate') || 'null') === null) {
        localStorage.setItem('cate', JSON.stringify("food"));
    }
    
    console.log(JSON.parse(localStorage.getItem('cate')));
    
    let cate = JSON.parse(localStorage.getItem('cate'));
    
    let homeMainSection = document.querySelector('.home-main-section');

    document.querySelector(`.nav-link[code-name='${cate}']`).click();
};
