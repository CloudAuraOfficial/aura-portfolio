// Scroll-reveal: fade-in sections as they enter viewport
(function () {
    const observer = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add('revealed');
                observer.unobserve(entry.target);
            }
        });
    }, { threshold: 0.12, rootMargin: '0px 0px -40px 0px' });

    function observe() {
        document.querySelectorAll('.reveal').forEach(el => {
            if (!el.classList.contains('revealed')) {
                observer.observe(el);
            }
        });
    }

    // Initial + Blazor re-render
    observe();
    const mo = new MutationObserver(observe);
    mo.observe(document.body, { childList: true, subtree: true });
})();

// Dark mode toggle
window.toggleDarkMode = function () {
    document.documentElement.classList.toggle('dark');
    const isDark = document.documentElement.classList.contains('dark');
    localStorage.setItem('theme', isDark ? 'dark' : 'light');
};

// Apply saved theme
(function () {
    const saved = localStorage.getItem('theme');
    if (saved === 'dark' || (!saved && window.matchMedia('(prefers-color-scheme: dark)').matches)) {
        document.documentElement.classList.add('dark');
    }
})();
