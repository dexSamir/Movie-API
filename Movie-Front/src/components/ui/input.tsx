import * as React from "react";

export interface InputProps
  extends React.InputHTMLAttributes<HTMLInputElement> {}

const Input = React.forwardRef<HTMLInputElement, InputProps>(
  ({ className, type, ...props }, ref) => {
    return (
      <input
        type={type}
        className={`flex h-10 w-full rounded-md border text-black  border-gray-300 bg-white px-3 py-2 text-sm outline-none focus:border-orange-700 focus:ring-2 focus:ring-blue-700 focus:ring-opacity-50 placeholder-gray-400 disabled:cursor-not-allowed disabled:opacity-50 ${
          className || ""
        }`}
        ref={ref}
        {...props}
      />
    );
  }
);
Input.displayName = "Input";

export { Input };
