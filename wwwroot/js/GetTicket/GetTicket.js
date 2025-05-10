const themChang = document.getElementById("themChang");
const dsChang = document.getElementById("dsChang");

let countChang = 1;

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
    // Cập nhật lại tiêu đề số chặng
    Array.from(dsChang.children).forEach((seg, idx) => {
      seg.querySelector("h3").textContent = `Chặng bay ${idx + 2}`;
    });
  });
});

    
    