window.addEventListener('scroll', reveal);
function reveal() {
    //Animacje z osi Y
    if (window.innerWidth > 1638) {


        let revealsY = document.querySelectorAll('.revealY');

        for (let i = 0; i < revealsY.length; i++) {
            revealsY[i].style.cssText = '';
        }

        //Animacje z osi X

        let revealsX = document.querySelectorAll('.revealX');

        //Z prawej strony

        for (let i = 0; i < revealsX.length; i++) {
            revealsX[i].style.cssText = '';
        }

        //Z lewej strony

        let revealsXFromLeft = document.querySelectorAll('.revealsXFromLeft');

        for (let i = 0; i < revealsXFromLeft.length; i++) {
            revealsXFromLeft[i].style.cssText = '';
        }


        for (let i = 0; i < revealsY.length; i++) {
            let windowheight = window.innerHeight;
            let revealTop = revealsY[i].getBoundingClientRect().top;
            let revealPoint = 100;

            if (revealTop < windowheight - revealPoint) {
                revealsY[i].classList.add('active');
            }
            else {
                revealsY[i].classList.remove('active');
            }
        }


        //Animacje z osi X


        //Z prawej strony

        for (let i = 0; i < revealsX.length; i++) {
            let windowheight = window.innerHeight;
            let revealTop = revealsX[i].getBoundingClientRect().top;
            let revealPoint = 150;

            if (revealTop < windowheight - revealPoint) {
                revealsX[i].classList.add('transition-right');
            }
            else {
                revealsX[i].classList.remove('transition-right');
            }
        }

        //Z lewej strony



        for (let i = 0; i < revealsXFromLeft.length; i++) {
            let windowheight = window.innerHeight;
            let revealTop = revealsXFromLeft[i].getBoundingClientRect().top;
            let revealPoint = 150;

            if (revealTop < windowheight - revealPoint) {
                revealsXFromLeft[i].classList.add('transition-left');
            }
            else {
                revealsXFromLeft[i].classList.remove('transition-left');
            }
        }
    }
    else {

        let revealsY = document.querySelectorAll('.revealY');

        for (let i = 0; i < revealsY.length; i++) {
            revealsY[i].style.cssText = 'opacity: 1; transform: translateY(0); transition: none;';
        }

        //Animacje z osi X

        let revealsX = document.querySelectorAll('.revealX');

        //Z prawej strony

        for (let i = 0; i < revealsX.length; i++) {
            revealsX[i].style.cssText = 'opacity: 1; transform: translateX(0); transition: none;';
        }

        //Z lewej strony

        let revealsXFromLeft = document.querySelectorAll('.revealsXFromLeft');

        for (let i = 0; i < revealsXFromLeft.length; i++) {
            revealsXFromLeft[i].style.cssText = 'opacity: 1; transform: translateX(0); transition: none;';
        }
    }

    let opacityReveals = document.querySelectorAll('.opacityReveal');

    for (let i = 0; i < opacityReveals.length; i++) {
        let windowHeight = window.innerHeight;
        let opacityReveal = opacityReveals[i].getBoundingClientRect().top;
        let revealPoint = 130;

        if (opacityReveal < windowHeight - revealPoint) {
            opacityReveals[i].classList.add('Reveal');
        }
    }
}

let windowheight = window.innerHeight;
let revealPoint = 220;
let rotates = document.querySelector('.rotate');
let interval = setInterval(function () { rotates.getBoundingClientRect().top < windowheight - revealPoint ? rotate() : null }, 1000 / 60);
function rotate() {
    rotates.classList.add('startPosition');
    clearInterval(interval);
}