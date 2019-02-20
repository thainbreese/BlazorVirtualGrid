window.gridInterop = {
    getWidth: function (grid) {
        return grid.parentNode.clientWidth;
    },
    getHeight: function (grid) {
        return grid.parentNode.clientHeight;
    },
    getTop: function (e) {
        return e.getBoundingClientRect().top;
    },
    getLeft: function (e) {
        return e.getBoundingClientRect().left;
    }
};