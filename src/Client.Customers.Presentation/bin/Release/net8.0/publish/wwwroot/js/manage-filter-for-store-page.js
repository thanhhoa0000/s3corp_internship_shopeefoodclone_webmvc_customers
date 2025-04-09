function initializeFilterTag(checkboxes, filterTag) {
    checkboxes.forEach(checkbox => {
        checkbox.addEventListener("change", () => updateFilterTag(checkboxes, filterTag));
    });
}

function updateFilterTag(checkboxes, filterTag) {
    const anyChecked = Array.from(checkboxes).some(checkbox => checkbox.checked);
    filterTag.style.display = anyChecked ? "flex" : "none";
    const checkedCount = Array.from(checkboxes).filter(checkbox => checkbox.checked).length;
    filterTag.querySelector('span').textContent = `(${checkedCount})`;
}
