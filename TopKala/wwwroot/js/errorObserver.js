window.onload = function(){
    // hide error containers
    var errorContainers = document.querySelectorAll('.field-validation-valid, .field-validation-error');
    for (var error of errorContainers){
        error.classList.add('d-none');
    }
    var errorInputs = document.querySelectorAll('[data-error-input]');
    var errorOutputs = document.querySelectorAll('[data-error-output]');

    errorInputs.forEach(
        input => errorOutputs.forEach(
            output => input.dataset.errorInput == output.dataset.errorOutput ? 
                      attachObserver(input, output) : false));
}

function attachObserver(input, outputContainer){
    const observerConfig = { childList: true };

    var observer = new MutationObserver(function(){
        observerCallback(input, outputContainer);
    });

    observer.observe(input, observerConfig);
}

function observerCallback(input, outputContainer){
    var output = outputContainer.querySelector("i");
    if (input.firstChild){
        displayAnimation(outputContainer, true);
        output.setAttribute("data-original-title", input.firstChild.innerHTML);
    }
    else {
        displayAnimation(outputContainer, false);
    }
}