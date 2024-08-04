import AggregatedPollDataByEndDateChart from "@/components/AggregatedPollDataByEndDateChart";
import AggregatedPollDataByPollsterChart from "@/components/AggregatedPollDataByPollsterChart";
import LatestPollInfoBox from "@/components/LatestPollInfoBox";
import Loading from "@/components/Loading";
import OverallAverageInfoBox from "@/components/OverallAverageInfoBox";
import { Suspense } from "react";

export default function Home() {
  return (
    <main className="m-4">
      <div className="w-3/4 mx-auto flex flex-col gap-4 justify-center items-center">
        <div className="text-3xl">2024 Election Dashboard</div>
        <div className="text-sm">
          Created by Justin Simon. Check out{" "}
          <a
            href="https://github.com/jtsimon93/ElectionData"
            target="_blank"
            rel="noopener noreferrer"
            referrerPolicy="no-referrer"
            className="underline"
          >
            the project
          </a>
          ,{" "}
          <a
            href="https://www.linkedin.com/in/justintsimon/"
            target="_blank"
            rel="noopener noreferrer"
            referrerPolicy="no-referrer"
            className="underline"
          >
            hire me
          </a>
          .
        </div>
      </div>

      <div className="w-3/4 mx-auto flex flex-col md:flex-row gap-4 md:gap-10 justify-center items-center mt-5">
        <div className="flex-1">
          <Suspense fallback={<Loading />}>
            <LatestPollInfoBox />
          </Suspense>
        </div>
        <div className="flex-1">
          <Suspense fallback={<Loading />}>
            <OverallAverageInfoBox />
          </Suspense>
        </div>
      </div>

      <div className="w-full mx-auto mt-5 md:w-3/4">
        <Suspense fallback={<Loading />}>
          <AggregatedPollDataByEndDateChart />
        </Suspense>
      </div>

      <div className="w-full mx-auto mt-5 md:w-3/4">
        <Suspense fallback={<Loading />}>
          <AggregatedPollDataByPollsterChart /> 
        </Suspense> 
      </div>
    </main>
  );
}
