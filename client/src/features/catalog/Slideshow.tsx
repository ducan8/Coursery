import { Swiper, SwiperSlide } from "swiper/react";
import "swiper/css";
import "swiper/css/navigation";
import "swiper/css/pagination";
import { Navigation, Autoplay } from "swiper/modules";

export default function Slideshow() {
  const images = [
    "https://img-c.udemycdn.com/notices/web_carousel_slide/image/e6cc1a30-2dec-4dc5-b0f2-c5b656909d5b.jpg",
    "https://img-c.udemycdn.com/notices/web_carousel_slide/image/10ca89f6-811b-400e-983b-32c5cd76725a.jpg",
  ];

  return (
    <Swiper
      modules={[Navigation, Autoplay]}
      navigation
      loop={true}
      autoplay={{ delay: 3500, disableOnInteraction: false }}
      spaceBetween={30}
      slidesPerView={1}
      style={{ width: "90%", height: "450px" }}
    >
      {images.map((image, index) => (
        <SwiperSlide key={index}>
          <img
            src={image}
            alt={`Slide ${index + 1}`}
            style={{
              width: "100%",
              height: "100%",
              objectFit: "cover",
              zIndex: 80,
            }}
          />
        </SwiperSlide>
      ))}
    </Swiper>
  );
}
