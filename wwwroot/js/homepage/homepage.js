document.addEventListener("DOMContentLoaded", function () {

    
        const btnBangTien = document.querySelector(".btn.btn-primary.m-2"); // Nút "Bằng Tiền"
        const tabDatVe = document.getElementById("tab-datve"); // Thẻ div có id="tab-datve"
        const allTabs = document.querySelectorAll(".tab-pane"); // Tất cả các tab

        btnBangTien.addEventListener("click", function (event) {
            event.preventDefault(); // Ngăn chặn hành vi mặc định của thẻ <a>

            // Ẩn tất cả các tab
            allTabs.forEach(tab => tab.classList.remove("show", "active"));

            // Hiển thị tab "Đặt Vé"
            tabDatVe.classList.add("show", "active");
        });
    

    // === Chuyển tab theo URL hash khi tải trang ===
    const hash = window.location.hash;
    if (hash) {
        let tab = document.querySelector(`a[href="${hash}"]`);
        if (tab) {
            new bootstrap.Tab(tab).show();
        }
    }

    // === Cập nhật hash khi click vào tab ===
    document.querySelectorAll('.nav-link').forEach(tab => {
        tab.addEventListener('click', function () {
            history.pushState(null, null, this.getAttribute('href'));
        });
    });

    // === Chuyển tab trong phần Làm Thủ Tục ===
    const checkinTabs = document.querySelectorAll("#checkin-tabs .nav-link");
    const inputField = document.getElementById("checkin-input");
    const labelField = document.getElementById("label-input");

    if (checkinTabs.length > 0 && inputField && labelField) {
        const tabData = {
            booking: { label: "MÃ ĐẶT CHỖ", placeholder: "123XXX" },
            ticket: { label: "SỐ VÉ", placeholder: "987654321" },
            member: { label: "SỐ HỘI VIÊN", placeholder: "80000xxxx" }
        };

        checkinTabs.forEach(tab => {
            tab.addEventListener("click", function (event) {
                event.preventDefault();

                checkinTabs.forEach(t => t.classList.remove("active"));
                this.classList.add("active");

                let tabType = this.getAttribute("data-type");
                labelField.textContent = tabData[tabType].label;
                inputField.placeholder = tabData[tabType].placeholder;
            });
        });
    }

    // === Ẩn/hiện Ngày Về khi chọn khứ hồi ===
    const oneWay = document.getElementById("oneWay");
    const roundTrip = document.getElementById("roundTrip");
    const multiCity = document.getElementById("multicity");
    const returnDateContainer = document.getElementById("return-date-container");

    function toggleReturnDate() {
        if (oneWay && returnDateContainer) {
            returnDateContainer.style.display = oneWay.checked ? "none" : "block";
        }
    }

    if (oneWay && roundTrip && multiCity) {
        oneWay.addEventListener("change", toggleReturnDate);
        roundTrip.addEventListener("change", toggleReturnDate);
        multiCity.addEventListener("change", toggleReturnDate);
        toggleReturnDate();
    }

    // === Nút đổi chiều điểm đi và điểm đến ===
    const swapButton = document.getElementById("swapButton");
    const fromDropdown = document.querySelector("#tab-datve .col-md-3:first-child .dropdown-toggle");
    const toDropdown = document.querySelector("#tab-datve .col-md-3:nth-child(3) .dropdown-toggle");

    if (swapButton && fromDropdown && toDropdown) {
        swapButton.addEventListener("click", function () {
            let tempText = fromDropdown.textContent;
            let tempValue = fromDropdown.getAttribute("data-value");

            fromDropdown.textContent = toDropdown.textContent;
            fromDropdown.setAttribute("data-value", toDropdown.getAttribute("data-value"));

            toDropdown.textContent = tempText;
            toDropdown.setAttribute("data-value", tempValue);
        });
    }

    document.querySelectorAll('.dropdown-item').forEach(item => {
        item.addEventListener('click', function () {
            let button = this.closest('.dropdown').querySelector('.dropdown-toggle');
            button.textContent = this.textContent;
            button.setAttribute('data-value', this.getAttribute('data-value'));
        });
    });

    // === Điều chỉnh số lượng hành khách ===
    const amountPassenger = document.getElementById("amountPassenger");
    const adultCountEl = document.getElementById("adultCount");
    const childCountEl = document.getElementById("childCount");
    const infantCountEl = document.getElementById("infantCount");

    function updatePassengerLabel() {
        if (amountPassenger && adultCountEl && childCountEl && infantCountEl) {
            let adult = parseInt(adultCountEl.textContent);
            let child = parseInt(childCountEl.textContent);
            let infant = parseInt(infantCountEl.textContent);
            amountPassenger.textContent = `${adult + child + infant}`;
        }
    }

    window.updateCount = function (type, change) {
        let countElement = type === "adult" ? adultCountEl : type === "child" ? childCountEl : infantCountEl;

        if (!countElement) return;
        let count = parseInt(countElement.textContent) + change;

        if (type === "adult" && count < 1) return;
        if ((type === "child" || type === "infant") && count < 0) return;
        if (type === "infant" && count > parseInt(adultCountEl.textContent)) {
            alert("Mỗi trẻ sơ sinh cần ít nhất một người lớn đi kèm.");
            return;
        }

        countElement.textContent = count;
        updatePassengerLabel();
    };

    if (amountPassenger) updatePassengerLabel();

    // === Giữ dropdown mở khi chọn số lượng hành khách ===
    const passengerSelect = document.getElementById("passengerSelect");
    const passengerBox = document.getElementById("passengerBox");

    if (passengerSelect && passengerBox) {
        passengerSelect.addEventListener("click", function (event) {
            event.stopPropagation();
            passengerBox.classList.add("show");
        });

        document.addEventListener("click", function (event) {
            if (!passengerSelect.contains(event.target) && !passengerBox.contains(event.target)) {
                passengerBox.classList.remove("show");
            }
        });
    }

    // === Giữ dropdown mở khi chọn điểm đi, điểm đến ===
    document.querySelectorAll('.keep-open').forEach(function (dropdown) {
        dropdown.addEventListener('click', function (event) {
            event.stopPropagation();
        });
    });

    document.querySelectorAll('.dropdown-item').forEach(item => {
        item.addEventListener('click', function () {
            let button = this.closest('.dropdown').querySelector('.dropdown-toggle');
            button.textContent = this.textContent;
            let dropdownMenu = this.closest('.dropdown-menu');
            dropdownMenu.classList.remove('show');
            let dropdown = this.closest('.dropdown');
            dropdown.classList.remove('show');
        });
    });
});
