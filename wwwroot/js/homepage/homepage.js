document.addEventListener("DOMContentLoaded", function () {
        // Xử lý hiển thị/ẩn ngày về khi chọn loại vé
        const roundTripRadio = document.getElementById('roundTrip');
        const oneWayRadio = document.getElementById('oneWay');
        const returnDateContainer = document.getElementById('return-date-container');

        if (roundTripRadio && oneWayRadio && returnDateContainer) {
            roundTripRadio.addEventListener('change', function() {
                if (this.checked) {
                    returnDateContainer.style.display = 'block';
                }
            });

            oneWayRadio.addEventListener('change', function() {
                if (this.checked) {
                    returnDateContainer.style.display = 'none';
                }
            });
        }
    
  
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

    

    // Xử lý logic Đa chặng chỉ khi có đủ phần tử
    const themChang = document.getElementById("themChang");
    const dsChang = document.getElementById("dsChang");
    let countChang = 1;
    // Chỉ xử lý Đa chặng nếu có đủ phần tử
    if (themChang && dsChang) {
      themChang.addEventListener("click", function () {
        countChang++;
        const newSegment = document.createElement("div");
        newSegment.classList.add("flight-segment", "mt-6", "p-4", "border", "rounded", "bg-gray-50");
        newSegment.innerHTML = `
          <h3 class="text-lg font-semibold text-gray-800 mb-2">Chặng bay ${countChang}</h3>
          <div class="grid grid-cols-1 md:grid-cols-2 gap-4">
            <div>
              <label class="block text-sm font-medium text-gray-700">Từ *</label>
              <input type="text" required class="mt-1 block w-full p-2 border rounded" placeholder="Nhập điểm đi" />
            </div>
            <div>
              <label class="block text-sm font-medium text-gray-700">Đến *</label>
              <input type="text" required class="mt-1 block w-full p-2 border rounded" placeholder="Nhập điểm đến" />
            </div>
            <div>
              <label class="block text-sm font-medium text-gray-700">Ngày khởi hành *</label>
              <input type="date" required class="mt-1 block w-full p-2 border rounded" />
            </div>
            <div class="flex items-end">
              <button class="remove-segment bg-red-500 hover:bg-red-600 text-white px-4 py-2 rounded mt-1">
                Xóa chặng
              </button>
            </div>
          </div>
        `;
        dsChang.appendChild(newSegment);
        newSegment.querySelector(".remove-segment").addEventListener("click", function () {
          dsChang.removeChild(newSegment);
          countChang--;
          Array.from(dsChang.children).forEach((seg, idx) => {
            seg.querySelector("h3").textContent = `Chặng bay ${idx + 2}`;
          });
        });
      });
    }

    // === Xử lý dropdown điểm đi/điểm đến bằng event delegation ===
    // Dropdown "Từ"
    const fromDropdownMenu = document.querySelector('#tabCountryFromContent').closest('.dropdown-menu') || document.querySelector('#tabCountryFromContent').parentElement;
    const fromDropdown = document.getElementById("dropdownFromBtn");
    // Dropdown "Tới"
    const toDropdownMenu = document.querySelector('#tabCountryToContent').closest('.dropdown-menu') || document.querySelector('#tabCountryToContent').parentElement;
    const toDropdown = document.getElementById("dropdownToBtn");
    
    // Hàm cập nhật text và value cho dropdown
    function setDropdownValue(button, text, value) {
        const textSpan = button.querySelector('.dropdown-text');
        if (textSpan) textSpan.textContent = text;
        button.setAttribute('data-value', value);
        // Force re-render: replace node
        const parent = button.parentNode;
        const next = button.nextSibling;
        parent.removeChild(button);
        if (next) {
            parent.insertBefore(button, next);
        } else {
            parent.appendChild(button);
        }
    }

    // Event delegation cho dropdown "Từ"
    if (fromDropdownMenu && fromDropdown) {
        fromDropdownMenu.addEventListener('click', function(e) {
            const item = e.target.closest('.dropdown-item');
            if (item) {
                setDropdownValue(fromDropdown, item.textContent, item.getAttribute('data-value'));
                console.log('Chọn điểm đi:', item.textContent, item.getAttribute('data-value'));
            }
        });
    }
    // Event delegation cho dropdown "Tới"
    if (toDropdownMenu && toDropdown) {
        toDropdownMenu.addEventListener('click', function(e) {
            const item = e.target.closest('.dropdown-item');
            if (item) {
                setDropdownValue(toDropdown, item.textContent, item.getAttribute('data-value'));
                console.log('Chọn điểm đến:', item.textContent, item.getAttribute('data-value'));
            }
        });
    }

    // === Giữ dropdown mở khi chọn điểm đi, điểm đến ===
    document.querySelectorAll('.keep-open').forEach(function (dropdown) {
        dropdown.addEventListener('click', function (event) {
            event.stopPropagation();
        });
    });
    
    

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


});

